using Godot;
using System;
using AutoLoads;
using Game;
using Game.WorldBuilding;

public class MouseBuildItemPreview : TextureRect
{

    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;

    private Vector2 _offSet = new Vector2(-64, -64);

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _globalEvents.Connect(nameof(GlobalEvents.BuildItemButtonClicked), this, nameof(OnBuildItemButtonClicked));
        _globalEvents.Connect(nameof(GlobalEvents.GameStateEntered), this, nameof(ResetTextureToNull));
    }

    public override void _Process(float delta)
    {
      RectPosition = GetViewport().GetMousePosition() + _offSet;
    }

    private void OnBuildItemButtonClicked(BuildItemResource buildItemResource)
    {
        Texture = buildItemResource.Icon;
    }

    private void ResetTextureToNull(GameStates gameState)
    {
        Texture = null;
    }
    
}
