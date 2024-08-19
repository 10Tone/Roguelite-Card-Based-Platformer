using Godot;
using System;
using Game.WorldBuilding;

public partial class WorldManager : Node2D
{
    [Export] private NodePath _worldBuildingPath;
    
    private WorldBuilding _worldBuilding;

    public override void _EnterTree()
    {
        base._EnterTree();
        _worldBuilding = GetNode<WorldBuilding>(_worldBuildingPath);
    }

    public void OnStageFinished()
    {
        _worldBuilding?.OnStageFinished();
    }
    
}