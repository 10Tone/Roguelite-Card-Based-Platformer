using System;
using AutoLoads;
using Game.LevelSystem;
using Godot;
using Godot.Collections;
using Tools;

namespace Game.WorldBuilding;

public partial class BuildGrid : TileMapLayer
{
    [Signal]
    public delegate void PlacementRequestedEventHandler(Vector2I tilePosition);

    private Dictionary<Vector2, Node2D> _buildedItems
    {
        get => _globalVariables.BuildedItems;
        set { _globalVariables.BuildedItems = value; }
    }

    private bool _buildingEnabled;

    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;

    private StageData _currentStageData;


    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _globalEvents.GameStateEntered += OnGameStateEntered;
    }

    public override void _Ready()
    {
        TileSet.SetTileSize(new Vector2I(_globalVariables.WorldGridCellSize, _globalVariables.WorldGridCellSize));
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!ShouldHandleInput(@event, out var eventMouseButton, out var cellPos))
            return;

        switch (eventMouseButton.ButtonIndex)
        {
            case MouseButton.Right:
                HandleRightClick(cellPos);
                break;
            case MouseButton.Left:
                HandleLeftClick(cellPos);
                break;
        }
    }


    private void OnGameStateEntered(GameState gameState) =>
    _buildingEnabled = gameState switch
    {
        _ when gameState == _globalVariables.GameStates[GameModeState.PlayModeState] => false,
        _ when gameState == _globalVariables.GameStates[GameModeState.BuildModeState] => true,
        _ => _buildingEnabled
    };


    private bool ShouldHandleInput(InputEvent @event, out InputEventMouseButton eventMouseButton, out Vector2 cellPos)
    {
        eventMouseButton = null;
        cellPos = Vector2.Zero;

        if (!_buildingEnabled || @event is not InputEventMouseButton { Pressed: true } mouseButton)
            return false;

        eventMouseButton = mouseButton;
        var mousePos = GetGlobalMousePosition();
        var localPos = ToLocal(mousePos);
        cellPos = LocalToMap(localPos);

        if (cellPos.Y == 0)
        {
            // DebugOverlay.Instance.DebugPrint("Cannot build in the first row");
            return false;
        }

        return true;
    }

    private void HandleRightClick(Vector2 cellPos)
    {
        if (!_buildedItems.ContainsKey(cellPos))
            return;

        RemoveBlock(cellPos);
    }

    private void HandleLeftClick(Vector2 cellPos)
    {
        if (_buildedItems.ContainsKey(cellPos))
        {
            // DebugOverlay.Instance.DebugPrint("Cell is already occupied " + cellPos);
            return;
        }

        RequestPlacement((Vector2I)cellPos);
    }

    private void RemoveBlock(Vector2 cellPos)
    {
        // DebugOverlay.Instance.DebugPrint("Removing block at position " + cellPos);
        var blockInstance = _buildedItems[cellPos];
        var (neighbors, _) = CheckSurroundingCells(cellPos);
        blockInstance.QueueFree();
        _buildedItems.Remove(cellPos);

        var item = (IBuildItem)blockInstance;
        _globalEvents.EmitSignal(nameof(GlobalEvents.ItemRemoved), item.BuildItemResource, blockInstance, neighbors);

        // Check if the removed block is a platform
        if (item.BuildItemResource.BuildItemType == BuildItemTypes.Platforms)
        {
            // Check surrounding cells for traps above the removed platform

            foreach (var neighbor in neighbors)
            {
                var neighborPos = neighbor.Key;
                var neighborNode = neighbor.Value;
                if (neighborNode is IBuildItem neighborItem &&
                    neighborItem.BuildItemResource.BuildItemType == BuildItemTypes.Traps &&
                    neighborPos == cellPos + new Vector2(0, -1)) // Check if the trap is directly above
                {
                    // DebugOverlay.Instance.DebugPrint("Removing trap at position " + neighborPos);
                    neighborNode.QueueFree();
                    _buildedItems.Remove(neighborPos);
                    _globalEvents.EmitSignal(nameof(GlobalEvents.ItemRemoved), neighborItem.BuildItemResource,
                        neighborNode, neighbors);
                }
            }
        }
    }

    private (Dictionary<Vector2, Node2D> neighbors, bool isPlatformBelow) CheckSurroundingCells(Vector2 cellPos)
    {
        Vector2[] surroundingCells =
        {
            new(-1, 0), new(1, 0),
            new(0, -1), new(0, 1),
            new(-1, -1), new(1, -1),
            new(-1, 1), new(1, 1)
        };

        var neighbors = new Dictionary<Vector2, Node2D>();
        var isPlatformBelow = false;

        foreach (var offset in surroundingCells)
        {
            var neighborPos = cellPos + offset;
            if (_buildedItems.TryGetValue(neighborPos, out var neighborNode))
            {
                neighbors[neighborPos] = neighborNode;
                if (neighborNode is IBuildItem neighbor &&
                    neighbor.BuildItemResource.BuildItemType == BuildItemTypes.Platforms)
                {
                    if (offset == new Vector2(0, 1)) isPlatformBelow = true;
                }
            }
        }

        return (neighbors, isPlatformBelow);
    }

    private void PlaceBlock(Vector2 cellPos, Dictionary<Vector2, Node2D> neighbors)
    {
        var blockInstance = _globalVariables.SelectedBuildItem?.Scene.Instantiate() as Node2D;
        if (blockInstance is null)
        {
            DebugOverlay.Instance.DebugPrint("BlockInstance is null");
            return;
        }

        blockInstance.Position = ToGlobal(MapToLocal((Vector2I)cellPos));
        AddChild(blockInstance);
        _buildedItems.Add(cellPos, blockInstance);

        var item = (IBuildItem)blockInstance;
        _globalEvents.EmitSignal(nameof(GlobalEvents.ItemBuild), item.BuildItemResource, blockInstance, neighbors);
    }

    private void RequestPlacement(Vector2I tilePosition)
    {
        EmitSignal(SignalName.PlacementRequested, tilePosition);
    }

    public void ConfirmPlacement(Vector2I tilePosition)
    {
        var neighborData = CheckSurroundingCells(tilePosition);
        if (_globalVariables.SelectedBuildItem.BuildItemType == BuildItemTypes.Traps &&
            !neighborData
                .isPlatformBelow)
        {
            return;
        }

        if (_globalVariables.SelectedBuildItem.BuildItemType == BuildItemTypes.Platforms)
        {
            var platformCount = 0;
            foreach (var item in _buildedItems.Values)
            {
                if (((IBuildItem)item).BuildItemResource.BuildItemType == BuildItemTypes.Platforms)
                {
                    platformCount++;
                }
            }

            if (platformCount >= _currentStageData.MaxPlaceablePlatforms )
            {
                return;
            }
        }

        PlaceBlock(tilePosition, neighborData.neighbors);
    }

    public void CancelPlacement(Vector2I tilePosition)
    {
        // DebugOverlay.Instance.DebugPrint("Placement at " + tilePosition + " is not allowed.");
    }

    public void ClearBuildGrid()
    {
        foreach (var (key, value) in _buildedItems)
        {
            value.QueueFree();
        }

        _buildedItems.Clear();
        _globalVariables.BuildedItems = _buildedItems;
    }

    public void SetCurrentStage(StageData stageData)
    {
        _currentStageData = stageData;
    }
}