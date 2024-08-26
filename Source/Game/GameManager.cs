using System;
using System.Collections.Generic;
using System.Linq;
using AutoLoads;
using Game.LevelSystem;
using Godot;
using Tools;

namespace Game;

public enum GameModeState
{
    PlayModeState,
    BuildModeState,
    DeathModeState,
    LevelFinishedModeState,
    StageFinishedModeState
}

public partial class GameManager : Node2D
{
    [Export] private int _worldGridCellSize;
    [Export] private PackedScene[] _levelScenes;
    
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    private GameStateMachine _gameStateMachine;

    private PlayModeState _playModeState;
    private BuildModeState _buildModeState;
    private DeathModeState _deathModeState;
    private LevelFinishedModeState _levelFinishedModeState;
    private StageFinishedModeState _stageFinishedModeState;

    private LevelManager _currentLevel;
    private int _currentLevelIndex;

    public override void _EnterTree()
    {
        _gameStateMachine = new GameStateMachine();
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _globalVariables.WorldGridCellSize = _worldGridCellSize;

        // _globalEvents.Connect(nameof(GlobalEvents.GameModeButtonPressedEventHandler), new Callable(this, nameof(OnGameModeButtonPressed)));
        // _globalEvents.Connect(nameof(GlobalEvents.PlayerFinishedLevelEventHandler), new Callable(this, nameof(OnPlayerFinishedLevel)));
        _globalEvents.GameModeButtonPressed += OnGameModeButtonPressed;
    }

    public override void _Ready()
    {
        _playModeState = new PlayModeState(_globalEvents, _globalVariables);
        _buildModeState = new BuildModeState(_globalEvents, _globalVariables);
        _deathModeState = new DeathModeState(_globalEvents, _globalVariables);
        _levelFinishedModeState = new LevelFinishedModeState(_globalEvents, _globalVariables);
        _stageFinishedModeState = new StageFinishedModeState(_globalEvents, _globalVariables);

        _globalVariables.GameStates.Add(GameModeState.PlayModeState, _playModeState);
        _globalVariables.GameStates.Add(GameModeState.BuildModeState, _buildModeState);
        _globalVariables.GameStates.Add(GameModeState.DeathModeState, _deathModeState);
        _globalVariables.GameStates.Add(GameModeState.LevelFinishedModeState, _levelFinishedModeState);
        _globalVariables.GameStates.Add(GameModeState.StageFinishedModeState, _stageFinishedModeState);

        LoadLevel(_currentLevelIndex);
        _gameStateMachine.Initialize(_buildModeState);
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

    private async void LoadLevel(int levelIndex)
    {
        _currentLevel?.QueueFree();
        
        _currentLevel = (LevelManager)_levelScenes[levelIndex].Instantiate();
        AddChild(_currentLevel);
        _currentLevel.LevelFinished += OnLevelFinished;
        _currentLevel.StageFinished += OnStageFinished;
        _currentLevel.PlayerDeath += OnPlayerDeath;
        _currentLevel.StageReady += OnStageReady;
        
        await ToSignal(_currentLevel, "ready");
        _globalEvents.EmitSignal(nameof(GlobalEvents.GameReady));
        
        if(_gameStateMachine.CurrentState != _buildModeState)
        {_gameStateMachine.ChangeState(_buildModeState);}
    }

    private void OnStageReady()
    {
        _gameStateMachine.ChangeState(_buildModeState);
    }

    private void OnStageFinished()
    {
        _gameStateMachine.ChangeState(_stageFinishedModeState);
    }

    private void OnGameModeButtonPressed()
{
    var currentGameState = _globalVariables.GameStates.FirstOrDefault(x => x.Value.GetType() == _gameStateMachine.CurrentState.GetType()).Key;

    switch (currentGameState)
    {
        case GameModeState.PlayModeState:
            _gameStateMachine.ChangeState(_buildModeState);
            break;
        case GameModeState.BuildModeState:
            _gameStateMachine.ChangeState(_playModeState);
            break;
        // Add other cases if needed
        default:
            // Handle unexpected states or do nothing
            break;
    }
}

    
    private void LoadNextLevel()
    {
		_currentLevelIndex++;
        LoadLevel(_currentLevelIndex);
    }

    private void OnLevelFinished()
    {
        _gameStateMachine.ChangeState(_levelFinishedModeState);
        LoadNextLevel();
    }

    private void OnPlayerDeath()
    {
        _gameStateMachine.ChangeState(_deathModeState);
    }
}