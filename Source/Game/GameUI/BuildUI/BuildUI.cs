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
    
    
    
    private void OnGameStateEntered(GameState gameState)
    {
        if (gameState == _globalVariables.GameStates["PlayModeState"])
        {
            Visible = false;
        }
        else if (gameState == _globalVariables.GameStates["BuildModeState"])
        {
            Visible = true;
        }
    }
}
