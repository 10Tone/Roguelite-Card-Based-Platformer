using AutoLoads;
using Godot;
using Godot.Collections;
using Tools;

namespace Game.WorldBuilding;

public partial class BuildGrid : TileMapLayer
{
    [Export()] private PackedScene _buildingBlockPath;

    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;

    private Dictionary<Vector2, Node2D> _buildedItems = new();

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
    }

    public override void _Ready()
    {
        TileSet.TileSize = _globalVariables.WorldGridSize;
    }


    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);

        if (@event is not InputEventMouseButton eventMouseButton)
        {
            return;
        }

        if (!eventMouseButton.Pressed)
        {
            return;
        }

        var mousePos = GetGlobalMousePosition();
        var localPos = ToLocal(mousePos);
        var cellPos = LocalToMap(localPos);
        var tileData = GetCellTileData(cellPos);
        
        DebugOverlay.Instance.DebugPrint(cellPos.ToString());
        
        switch (eventMouseButton.ButtonIndex)
        {
            // Changed from 2 to MouseButton.Right
            case MouseButton.Right when !_buildedItems.ContainsKey(cellPos):
                return;
            case MouseButton.Right:
            {
                DebugOverlay.Instance.DebugPrint("Removing block at position " + cellPos);
                var blockInstance = _buildedItems[cellPos];
                blockInstance.QueueFree();
                _buildedItems.Remove(cellPos);
                // SetCell(tilePos); // Changed from SetCellv
                //
                var item = (IBuildItem)blockInstance;
                _globalEvents.EmitSignal(nameof(GlobalEvents.ItemRemoved), item.BuildItemValue);
                break;
            }
            // Changed from 1 to MouseButton.Left
            // check if a key in local dictionary _buildItems exists based on the cellPos value
            case MouseButton.Left when _buildedItems.ContainsKey(cellPos):
                DebugOverlay.Instance.DebugPrint("Cell is already occupied " + cellPos);
                return;
            case MouseButton.Left:
            {
                var blockInstance =
                    _globalVariables.SelectedBuildItem?.Scene
                        .Instantiate() as Node2D; // Changed from Instance to Instantiate
                if (blockInstance is null)
                {
                    DebugOverlay.Instance.DebugPrint("BlockInstance is null");
                    return;
                }
                var offset = new Vector2(_globalVariables.WorldGridSize.X + (_globalVariables.WorldGridSize.X * 0.0625f),
                    _globalVariables.WorldGridSize.Y);
                
                blockInstance.Position = (ToGlobal(MapToLocal(cellPos)) - offset * 0.5f);
                AddChild(blockInstance);
                // SetCell(cellPos, 0); // Changed from SetCellv
                _buildedItems.Add(cellPos, blockInstance);
        
                var item = (IBuildItem)blockInstance;
                _globalEvents.EmitSignal(nameof(GlobalEvents.ItemBuild), item.BuildItemValue);
                break;
            }
        }
    }
    
}