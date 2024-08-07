using System;
using AutoLoads;
using Godot;
using Godot.Collections;
using Tools;

namespace Game.WorldBuilding;

public partial class BuildGrid : TileMapLayer
{
    [Signal]
    public delegate void PlacementRequestedEventHandler(Vector2I tilePosition);

    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    private Dictionary<Vector2, Node2D> _buildedItems = new();
    private bool _buildingEnabled;

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _globalEvents.GameStateEntered += OnGameStateEntered;
    }

    public override void _Ready()
    {
        TileSet.SetTileSize(_globalVariables.WorldGridSize);
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

    private void OnGameStateEntered(GameStates gamestate)
    {
        switch (gamestate)
        {
            case GameStates.PlayMode:
                _buildingEnabled = false;
                break;
            case GameStates.BuildMode:
                _buildingEnabled = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gamestate), gamestate, null);
        }
    }

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
            DebugOverlay.Instance.DebugPrint("Cannot build in the first row");
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
            DebugOverlay.Instance.DebugPrint("Cell is already occupied " + cellPos);
            return;
        }

        RequestPlacement((Vector2I)cellPos);
    }

    private void RemoveBlock(Vector2 cellPos)
    {
        DebugOverlay.Instance.DebugPrint("Removing block at position " + cellPos);
        var blockInstance = _buildedItems[cellPos];
        blockInstance.QueueFree();
        _buildedItems.Remove(cellPos);

        var item = (IBuildItem)blockInstance;
        // _globalEvents.EmitSignal(nameof(GlobalEvents.ItemRemoved), item.BuildItemValue);
    }

    private void CheckSurroundingCells(Vector2 cellPos)
    {
        Vector2[] surroundingCells = new Vector2[]
        {
            new Vector2(-1, 0), new Vector2(1, 0),
            new Vector2(0, -1), new Vector2(0, 1),
            new Vector2(-1, -1), new Vector2(1, -1),
            new Vector2(-1, 1), new Vector2(1, 1)
        };

        foreach (var offset in surroundingCells)
        {
            var neighborPos = cellPos + offset;
            if (_buildedItems.TryGetValue(neighborPos, out var neighborNode))
            {
                if (neighborNode is IBuildItem neighbor)
                {
                    GD.Print(neighbor.BuildItemResource.BuildItemType);
                    // DebugOverlay.Instance.DebugPrint($"Neighbor at {neighborPos}: {neighbor.BuildItemResource.BuildItemType}");
                }
            }
        }
    }

    private void PlaceBlock(Vector2 cellPos)
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
        // _globalEvents.EmitSignal(nameof(GlobalEvents.ItemBuild), item.BuildItemValue);
    }

    private void RequestPlacement(Vector2I tilePosition)
    {
        EmitSignal(SignalName.PlacementRequested, tilePosition);
    }

    public void ConfirmPlacement(Vector2I tilePosition)
    {
        CheckSurroundingCells(tilePosition);
        PlaceBlock(tilePosition);
    }

    public void CancelPlacement(Vector2I tilePosition)
    {
        DebugOverlay.Instance.DebugPrint("Placement at " + tilePosition + " is not allowed.");
    }
}