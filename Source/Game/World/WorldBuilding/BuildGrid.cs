using System;
using Godot;

namespace Game;

public class BuildGrid : TileMap
{
    [Export()] private PackedScene _buildingBlockPath;
    
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
        if (@event is not InputEventMouseButton { ButtonIndex: 1 }) return;
        var mousePos = GetViewport().GetMousePosition();
        var tilePos = WorldToMap(mousePos);

        var cell = GetCellv(tilePos);
        if (cell >= 0) { GD.Print("cell is null");return;}

        var blockInstance = _buildingBlockPath.Instance() as StaticBody2D;
        if (blockInstance is null)
        {
            GD.Print("BlockInstance is null");
            return;
        }
        blockInstance.Position = MapToWorld(tilePos) * Scale;
        AddChild(blockInstance);
        SetCellv(tilePos, 0);
        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}