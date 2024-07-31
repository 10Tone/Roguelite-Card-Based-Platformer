using AutoLoads;
using Game.WorldBuilding;
using Godot;
using Tools;

namespace Game;

public partial class BuildItemButton : TextureRect
{
    [Export()] private BuildItemResource _buildItemResource;
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
    }
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Texture = _buildItemResource.Icon;
        DebugOverlay.Instance.DebugPrint($"Clicked {_buildItemResource.Name}");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _on_BuildItemButton_gui_input(InputEvent @event)
    {
        if (@event is not InputEventMouseButton eventMouseButton) return;
        if (eventMouseButton.Pressed && eventMouseButton.ButtonIndex == MouseButton.Left)
        {
            _globalEvents.EmitSignal(nameof(GlobalEvents.BuildItemButtonClicked), _buildItemResource);
            _globalVariables.SelectedBuildItem = _buildItemResource;
        }

    }
}