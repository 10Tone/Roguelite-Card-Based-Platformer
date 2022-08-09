using System;
using AutoLoads;
using Godot;

namespace Game;

public class GameManager : Node2D
{
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

        _globalEvents.Connect(nameof(GlobalEvents.GameModeButtonPressed), this, nameof(OnGameModeButtonPressed));
    }

    public override void _Ready()
    {
        _globalEvents.EmitSignal(nameof(GlobalEvents.GameReady));

        _playModeState = new PlayModeState(_globalEvents, _globalVariables);
        _buildModeState = new BuildModeState(_globalEvents, _globalVariables);
        
        _gameStateMachine.Initialize((_buildModeState));
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        _gameStateMachine.CurrentState.LogicUpdate(delta);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        _gameStateMachine.CurrentState.PhysicsUpdate(delta);
    }

    private void OnGameModeButtonPressed(GameStates currentGameState)
    {
        GD.Print("received signal from UI " + currentGameState);
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
}