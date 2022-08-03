using AutoLoads;
using Godot;
using Godot.Collections;

namespace Game.WorldBuilding;

public class BuildGrid : TileMap
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
        // foreach (Vector2 cellPos in GetUsedCells())
        // {
        //     var cell = GetCellv(cellPos);
        //     GD.Print("tile with index " + cell + " is at position " + cellPos);
        // }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);

        if (@event is not InputEventMouseButton eventMouseButton) { return;}

        if (!eventMouseButton.Pressed) {return;}
        
        var mousePos = GetViewport().GetMousePosition();
        var tilePos = WorldToMap(mousePos);

        var cell = GetCellv(tilePos);

        if (eventMouseButton.ButtonIndex == 2)
        {
            if (cell < 0) { return;}
            GD.Print("remove block");
            var item = _buildedItems[tilePos];
            // item.CallDeferred("QueueFree");
            item.QueueFree();
            _buildedItems.Remove(tilePos);
            SetCellv(tilePos, -1);
        }

        else if (eventMouseButton.ButtonIndex == 1)
        {
            if (cell >= 0) { GD.Print("cell is not empty");return;}
            var blockInstance = _globalVariables.SelectedBuildItem?.Scene.Instance() as Node2D;
            if (blockInstance is null)
            {
                GD.Print("BlockInstance is null");
                return;
            }
            blockInstance.Position = MapToWorld(tilePos) * Scale;
            AddChild(blockInstance);
            SetCellv(tilePos, 0);
            _buildedItems.Add(tilePos, blockInstance);
        }
        
        
        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}