using System;
using AutoLoads;
using Godot;
using Tools;

namespace Game.GameplayUI;

public partial class GameplayUI: CanvasLayer
{
    [Export()] private NodePath _gameModeButtonPath;
    // private GameStates _gameState;
    private Button _gameModeButton;
    
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    
    

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        // _globalEvents.Connect(nameof(GlobalEvents.GameStateEnteredEventHandler), new Callable(this, nameof(OnGameStateEntered)));
        _globalEvents.GameStateEntered += OnGameStateEntered;
    }

    public override void _Ready()
    {
        _gameModeButton = GetNode(_gameModeButtonPath) as Button;
        // _gameModeButton?.Connect("pressed", new Callable(this, nameof(OnGameModeButtonPressed)));
        if(_gameModeButton == null) 
        {
            GD.PushWarning("GameModeButton is null!");
            return;
        }
        _gameModeButton.Pressed += OnGameModeButtonPressed;
    }
    
    
    private void OnGameStateEntered(GameState gameState)
{
    _gameModeButton.Text = gameState switch
    {
        _ when gameState == _globalVariables.GameStates[GameModeState.PlayModeState] => "Build",
        _ when gameState == _globalVariables.GameStates[GameModeState.BuildModeState] => "Play",
        _ => _gameModeButton.Text
    };
}

    


    private void OnGameModeButtonPressed()
    {
        _globalEvents.EmitSignal(nameof(GlobalEvents.GameModeButtonPressed));
    }
}