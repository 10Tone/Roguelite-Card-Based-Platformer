using AutoLoads;
using Game.WorldBuilding;
using Godot;
using Tools;

namespace Game;

public partial class BuildItemButton : TextureRect
{
    private BuildItemResource _buildItemResource;
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
    }
    

    public void LoadButtonWithData(BuildItemResource buildItemResource)
    {
        if(buildItemResource == null)
        {
            return;
        }
        
        _buildItemResource = buildItemResource;

        Texture = _buildItemResource.Icon;
    }


    public void _on_BuildItemButton_gui_input(InputEvent @event)
    {
        if (@event is not InputEventMouseButton eventMouseButton) return;
        if (eventMouseButton.Pressed && eventMouseButton.ButtonIndex == MouseButton.Left)
        {
            DebugOverlay.Instance.DebugPrint($"Clicked {_buildItemResource.Name}");
            _globalEvents.EmitSignal(nameof(GlobalEvents.BuildItemButtonClicked), _buildItemResource);
            _globalVariables.SelectedBuildItem = _buildItemResource;
        }

    }
}