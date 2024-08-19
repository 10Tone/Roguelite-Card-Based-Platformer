using System;
using System.Collections.Generic;
using System.Linq;
using AutoLoads;
using Game.LevelSystem;
using Godot;
using Tools;

namespace Game;

public partial class GameManager : Node2D
{
    [Export] private Vector2I _worldGridSize;
    [Export] private PackedScene[] _levelScenes;
    [Export] private NodePath _SubViewportNodePath;
    
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    private GameStateMachine _gameStateMachine;

    private PlayModeState _playModeState;
    private BuildModeState _buildModeState;
    private DeathModeState _deathModeState;
    private LevelFinishedModeState _levelFinishedModeState;

    private LevelManager _currentLevel;
    private SubViewport _subViewport;
    private int _currentLevelIndex;

    public override void _EnterTree()
    {
        _gameStateMachine = new GameStateMachine();
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _globalVariables.WorldGridSize = _worldGridSize;
        
        _subViewport = GetNode<SubViewport>(_SubViewportNodePath);
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


        _globalVariables.GameStates.Add("PlayModeState", _playModeState);
        _globalVariables.GameStates.Add("BuildModeState", _buildModeState);
        _globalVariables.GameStates.Add("DeathModeState", _deathModeState);
        _globalVariables.GameStates.Add("LevelFinishedModeState", _levelFinishedModeState);

        LoadLevel(_currentLevelIndex);
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

    private async void LoadLevel(int levelIndex)
    {
        _currentLevel?.QueueFree();
        
        _currentLevel = (LevelManager)_levelScenes[levelIndex].Instantiate();
        _subViewport?.AddChild(_currentLevel);
        _currentLevel.LevelFinished += OnLevelFinished;
        _currentLevel.StageFinished += OnStageFinished;
        _currentLevel.PlayerDeath += OnPlayerDeath;
        
        await ToSignal(_currentLevel, "ready");
        _globalEvents.EmitSignal(nameof(GlobalEvents.GameReady));
        
        if(_gameStateMachine.CurrentState != _playModeState)
        {_gameStateMachine.ChangeState(_playModeState);}
    }

    private void OnStageFinished()
    {
        throw new NotImplementedException();
    }

    private void OnGameModeButtonPressed()
    {
        var currentGameState = _globalVariables.GameStates.Keys.FirstOrDefault(x => _globalVariables.GameStates[x].GetType() == _gameStateMachine.CurrentState.GetType());
        
        if (currentGameState == "PlayModeState")
            _gameStateMachine.ChangeState(_buildModeState);
        else if (currentGameState == "BuildModeState")
            _gameStateMachine.ChangeState(_playModeState);
        
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