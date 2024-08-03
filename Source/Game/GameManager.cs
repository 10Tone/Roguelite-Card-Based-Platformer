using System;
using AutoLoads;
using Godot;
using Tools;

namespace Game;

public partial class GameManager : Node2D
{
    [Export] private Vector2I _worldGridSize;
    
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    private GameStateMachine _gameStateMachine;

    private PlayModeState _playModeState;
    private BuildModeState _buildModeState;

    public override void _EnterTree()
    {
        _gameStateMachine = new GameStateMachine();
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _globalVariables.WorldGridSize = _worldGridSize;

        // _globalEvents.Connect(nameof(GlobalEvents.GameModeButtonPressedEventHandler), new Callable(this, nameof(OnGameModeButtonPressed)));
        // _globalEvents.Connect(nameof(GlobalEvents.PlayerFinishedLevelEventHandler), new Callable(this, nameof(OnPlayerFinishedLevel)));
        _globalEvents.GameModeButtonPressed += OnGameModeButtonPressed;
        _globalEvents.PlayerFinishedLevel += OnPlayerFinishedLevel;
    }

    public override void _Ready()
    {
        _globalEvents.EmitSignal(nameof(GlobalEvents.GameReady));

        _playModeState = new PlayModeState(_globalEvents, _globalVariables);
        _buildModeState = new BuildModeState(_globalEvents, _globalVariables);
        
        _gameStateMachine.Initialize((_playModeState));
        
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        _gameStateMachine.CurrentState.LogicUpdate(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        _gameStateMachine.CurrentState.PhysicsUpdate(delta);
    }

    private void OnGameModeButtonPressed()
    {
        // check if _gameStateMachine.CurrentState is equal to _playModeState or _buildModeState

        GameStates currentGameState = _gameStateMachine.CurrentState is PlayModeState? GameStates.PlayMode : GameStates.BuildMode;
        
        switch (currentGameState)
        {
            case GameStates.PlayMode:
                _gameStateMachine.ChangeState(_buildModeState);
                break;
            case GameStates.BuildMode:
                _gameStateMachine.ChangeState(_playModeState);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(currentGameState), currentGameState, null);
        }
    }

    private void OnPlayerFinishedLevel()
    {
        _gameStateMachine.ChangeState(_buildModeState);
    }
}