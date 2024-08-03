using Godot;
using System;
using AutoLoads;
using Game;

public partial class BuildUI: Control
{
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    
    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _globalEvents.GameStateEntered += OnGameStateEntered;
    }
    
    
    private void OnGameStateEntered(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.PlayMode:
                Visible = false;
                break;
            case GameStates.BuildMode:
                Visible = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
    }
}
