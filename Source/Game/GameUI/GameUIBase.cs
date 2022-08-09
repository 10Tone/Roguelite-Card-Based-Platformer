using System;
using AutoLoads;
using Godot;

namespace Game;

public class GameUIBase : CanvasLayer
{
    [Export()] private NodePath _gameModeButtonPath;
    [Export()] protected GameStates GameState;
    private Button _gameModeButton;
    
    protected GlobalEvents _globalEvents;
    protected GlobalVariables _globalVariables;
    
    

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _globalEvents.Connect(nameof(GlobalEvents.GameStateEntered), this, nameof(OnGameStateEntered));
    }

    public override void _Ready()
    {
        _gameModeButton = GetNode(_gameModeButtonPath) as Button;
        _gameModeButton?.Connect("pressed", this, nameof(OnGameModeButtonPressed));
        GD.Print("UI: " + GameState);
    }

    protected virtual void OnGameStateEntered(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.PlayMode:
                break;
            case GameStates.BuildMode:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
    }

    protected void OnGameModeButtonPressed()
    {
        _globalEvents.EmitSignal(nameof(GlobalEvents.GameModeButtonPressed), GameState);
    }
}