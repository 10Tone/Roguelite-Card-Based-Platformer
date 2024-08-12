using Godot;
using System;
using AutoLoads;
using Game;
using Game.WorldBuilding;

public partial class MouseBuildItemPreview : Sprite2D
{

    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;

    // private Vector2 _offSet = new Vector2(-16, -16);

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        // _globalEvents.Connect(nameof(GlobalEvents.BuildItemButtonClickedEventHandler), new Callable(this, nameof(OnBuildItemButtonClicked)));
        // _globalEvents.Connect(nameof(GlobalEvents.GameStateEnteredEventHandler), new Callable(this, nameof(ResetTextureToNull)));
        
        _globalEvents.BuildItemButtonClicked += OnBuildItemButtonClicked;
        _globalEvents.GameStateEntered += ResetTextureToNull;
        
        Texture = null;
    }

    public override void _Process(double delta)
    {
      // Position = GetViewport().GetMousePosition() + _offSet;
      Position = GetGlobalMousePosition();
    }

    private void OnBuildItemButtonClicked(BuildItemResource buildItemResource)
    {
        Texture = buildItemResource.Icon;
    }

    private void ResetTextureToNull(Tools.State gameState)
    {
        Texture = null;
    }
    
}
