using AutoLoads;
using Godot;
using Tools;

namespace Game;

public partial class BuildMenu : Control
{
    [Export] private NodePath _buildButtonsPath;
    [Export] private PackedScene _buildItemButtonScene;
    
    private GridContainer _buildButtonsContainer;
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        
        _globalEvents.GameReady += OnGameReady;
    }

    private void OnGameReady()
    {
        if (_buildButtonsContainer == null)
        {
            _buildButtonsContainer = GetNode<GridContainer>(_buildButtonsPath);
        }
        
        DebugOverlay.Instance.DebugPrint("build buttons");
        CreateBuildButtons();
    }

    public override void _Ready()
    {
        _buildButtonsContainer = GetNode<GridContainer>(_buildButtonsPath);
    }

    private void CreateBuildButtons()
    {
        RemoveAllButtons();
        foreach (var item in _globalVariables.BuildItemResources)
        {
            if (!item.IsUnlocked) continue;
            var buildItemButton = (BuildItemButton)_buildItemButtonScene.Instantiate();
            buildItemButton.LoadButtonWithData(item);
            _buildButtonsContainer.AddChild(buildItemButton);
        }
        
    }

    private void RemoveAllButtons()
    {
        foreach (var child in _buildButtonsContainer.GetChildren())
        {
            child.QueueFree();
        }
    }
    
}