using AutoLoads;
using Godot;

namespace Game;

public partial class BuildMenu : Control
{
    [Export] private NodePath _buildButtonsPath;
    [Export] private PackedScene _buildItemButtonScene;
    
    private GridContainer _buildButtonsContainer;
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    public override void _Ready()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _buildButtonsContainer = GetNode<GridContainer>(_buildButtonsPath);
        CreateBuildButtons();
    }

    private void CreateBuildButtons()
    {
        foreach (var item in _globalVariables.BuildItemResources)
        {
            var buildItemButton = (BuildItemButton)_buildItemButtonScene.Instantiate();
            buildItemButton.LoadButtonWithData(item);
            _buildButtonsContainer.AddChild(buildItemButton);
        }
        
    }
}