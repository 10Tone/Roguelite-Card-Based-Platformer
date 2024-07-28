using AutoLoads;
using Godot;
using Godot.Collections;

namespace Game.WorldBuilding;

public partial class BuildGrid : TileMap
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

    if (@event is not InputEventMouseButton eventMouseButton) { return; }

    if (!eventMouseButton.Pressed) { return; }
    
    var mousePos = GetViewport().GetMousePosition();
    var tilePos = LocalToMap(mousePos);

    var cell = GetCellAtlasCoords(0, tilePos); // Changed from GetCellv

    if (eventMouseButton.ButtonIndex == MouseButton.Right) // Changed from 2 to MouseButton.Right
    {
        if (cell == Vector2I.Zero) { return; } // Changed comparison
        GD.Print("remove block");
        var blockInstance = _buildedItems[tilePos];
        blockInstance.QueueFree();
        _buildedItems.Remove(tilePos);
        SetCell(0, tilePos); // Changed from SetCellv
        
        var item = (IBuildItem)blockInstance;
        _globalEvents.EmitSignal(nameof(GlobalEvents.ItemRemoved), item.BuildItemValue);
    }
    else if (eventMouseButton.ButtonIndex == MouseButton.Left) // Changed from 1 to MouseButton.Left
    {
        if (cell != Vector2I.Zero) { GD.Print("cell is not empty"); return; } // Changed comparison
        var blockInstance = _globalVariables.SelectedBuildItem?.Scene.Instantiate() as Node2D; // Changed from Instance to Instantiate
        if (blockInstance is null)
        {
            GD.Print("BlockInstance is null");
            return;
        }
        blockInstance.Position = ToGlobal(MapToLocal(tilePos)) * Scale; // Changed from MapToWorld
        AddChild(blockInstance);
        SetCell(0, tilePos, 0); // Changed from SetCellv
        _buildedItems.Add(tilePos, blockInstance);

        var item = (IBuildItem)blockInstance;
        _globalEvents.EmitSignal(nameof(GlobalEvents.ItemBuild), item.BuildItemValue);
    }
}       

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}