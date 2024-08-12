using System;
using System.Collections.Generic;
using System.Linq;
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
    private DeathModeState _deathModeState;
    private LevelFinishedModeState _levelFinishedModeState;

    public override void _EnterTree()
    {
        _gameStateMachine = new GameStateMachine();
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _globalVariables.WorldGridSize = _worldGridSize;

        // _globalEvents.Connect(nameof(GlobalEvents.GameModeButtonPressedEventHandler), new Callable(this, nameof(OnGameModeButtonPressed)));
        // _globalEvents.Connect(nameof(GlobalEvents.PlayerFinishedLevelEventHandler), new Callable(this, nameof(OnPlayerFinishedLevel)));
        _globalEvents.GameModeButtonPressed += OnGameModeButtonPressed;
        _globalEvents.LevelFinished += OnLevelFinished;
        _globalEvents.PlayerDeath += OnPlayerDeath;
    }

    public override void _Ready()
    {
        _globalEvents.EmitSignal(nameof(GlobalEvents.GameReady));
        _playModeState = new PlayModeState(_globalEvents, _globalVariables);
        _buildModeState = new BuildModeState(_globalEvents, _globalVariables);
        _deathModeState = new DeathModeState(_globalEvents, _globalVariables);
        _levelFinishedModeState = new LevelFinishedModeState(_globalEvents, _globalVariables);
        
        
        _globalVariables.GameStates.Add("PlayModeState", _playModeState);
        _globalVariables.GameStates.Add("BuildModeState", _buildModeState);
        _globalVariables.GameStates.Add("DeathModeState", _deathModeState);
        _globalVariables.GameStates.Add("LevelFinishedModeState", _levelFinishedModeState);
        
        _gameStateMachine.Initialize(_playModeState);
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
        var currentGameState = _globalVariables.GameStates.Keys.FirstOrDefault(x => _globalVariables.GameStates[x].GetType() == _gameStateMachine.CurrentState.GetType());
        
        if (currentGameState == "PlayModeState")
            _gameStateMachine.ChangeState(_buildModeState);
        else if (currentGameState == "BuildModeState")
            _gameStateMachine.ChangeState(_playModeState);

        
        //
        // switch (currentGameState)
        // {
        //     case "PlayModeState":
        //         _gameStateMachine.ChangeState(_buildModeState);
        //         break;
        //     case "BuildModeState":
        //         _gameStateMachine.ChangeState(_playModeState);
        //         break;
        //     default:
        //         throw new ArgumentOutOfRangeException(nameof(currentGameState), currentGameState, null);
        // }
    }

    private void OnLevelFinished()
    {
        _gameStateMachine.ChangeState(_levelFinishedModeState);
    }

    private void OnPlayerDeath()
    {
        _gameStateMachine.ChangeState(_deathModeState);
    }
}