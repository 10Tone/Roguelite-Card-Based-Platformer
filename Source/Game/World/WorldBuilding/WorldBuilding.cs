using System.Collections.Generic;
using System.Linq;
using AutoLoads;
using Game.LevelSystem;
using Godot;
using Godot.Collections;

namespace Game.WorldBuilding;

public partial class WorldBuilding: Node2D
{
    [Export] private NodePath _buildGridPath;
    [Export] private NodePath _staticWorldBlocksPath;
    
    private BuildGrid _buildGrid;
    private StaticWorldBlocks _staticWorldBlocks;
    private GlobalVariables _globalVariables;


    public override void _EnterTree()
    {
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
    }

    public override void _Ready()
    {
        // Get references to the TileMaps
        _buildGrid = GetNode<BuildGrid>(_buildGridPath);
        _staticWorldBlocks = GetNode<StaticWorldBlocks>(_staticWorldBlocksPath);

        // Connect to the signal from BuildGrid
        _buildGrid.PlacementRequested += OnPlacementRequested;
    }

    // Method called when BuildGrid requests a placement
    private void OnPlacementRequested(Vector2I tilePosition)
    {
        if (_staticWorldBlocks.IsPositionAvailable(tilePosition))
        {
            // If the position is available, allow BuildGrid to proceed with placement
            _buildGrid.ConfirmPlacement(tilePosition);
        }
        else
        {
            // If the position is not available, inform BuildGrid
            _buildGrid.CancelPlacement(tilePosition);
        }
    }

    public void OnStageFinished()
    {
        _buildGrid?.ClearBuildGrid();
    }

    public void SetCurrentStage(StageData stageData)
    {
        _buildGrid?.SetCurrentStage(stageData);
        List<BuildItemResource> blocksToUnlock = new List<BuildItemResource> { stageData.BuildItemToUnlock };
        UnlockBuildingBlocks(blocksToUnlock);
    }
    
    private void UnlockBuildingBlocks(List<BuildItemResource> blocksToUnlock)
    {
        if (_globalVariables == null || _globalVariables.BuildItemResources == null)
        {
            // GD.PrintErr("GlobalVariables or BuildItemResources is null");
            return;
        }

        foreach (var buildItem in blocksToUnlock)
        {
            if (buildItem == null || string.IsNullOrEmpty(buildItem.Name))
            {
                // GD.PrintErr("BuildItem or its Name is null");
                continue;
            }

            var buildItemResource = _globalVariables.BuildItemResources.FirstOrDefault(r => r != null && r.Name == buildItem.Name);
            if (buildItemResource != null)
            {
                buildItemResource.IsUnlocked = true;
            }
            else
            {
                GD.PrintErr($"BuildItemResource with name {buildItem.Name} not found");
            }
        }
    }
}