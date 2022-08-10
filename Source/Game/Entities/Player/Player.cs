using System;
using AutoLoads;
using Game.PlayerStates;
using Godot;

namespace Game;

public class Player : KinematicBody2D, IPlayer
{
    [Export()] private PlayerData _playerData;
    [Export()] private NodePath _inputHandlerPath;

    public PlayerStateMachine PlayerStateMachine { get; set; }
    public PlayerData PlayerData { get; set; }
    public PlayerInputHandler InputHandler { get; set; }
    public Vector2 Motion { get; set; }
    public bool IsGrounded { get; set; }

    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    
    private IdleState _idleState;
    private MoveState _moveState;
    private JumpState _jumpState;

    public override void _EnterTree()
    {
        PlayerStateMachine = new PlayerStateMachine();
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _playerData = (PlayerData)GD.Load("res://Source/Game/Entities/Player/Data/PlayerData.tres");

        _globalEvents.Connect(nameof(GlobalEvents.GameStateEntered), this, nameof(OnGameStateEntered));
    }

    public override void _Ready()
    {
        AddToGroup("Player");
        PlayerData = _playerData;
        if(PlayerData is null) {GD.PushWarning("PlayerData is null!");}
        InputHandler = GetNode(_inputHandlerPath) as PlayerInputHandler;
        if(InputHandler is null) {GD.PushWarning("InputHandler is null!");}
       
        AddStates();
    }
    
    public override void _Process(float delta) 
    {
        PlayerStateMachine.CurrentState.LogicUpdate(delta);
    }

    public override void _PhysicsProcess(float delta)
    {
        PlayerStateMachine.CurrentState.PhysicsUpdate(delta);
        IsGrounded = IsOnFloor();
        var motion = Motion;
        motion.y += PlayerData.Gravity * delta;
        Motion = MoveAndSlide(motion, Vector2.Up, false, 4, 0f);
       
    }

    private void AddStates()
    {
        PlayerStateMachine.States.Add(PlayerStates.PlayerStates.Idle, _idleState = new IdleState(this, PlayerStates.PlayerStates.Idle.ToString()));
        PlayerStateMachine.States.Add(PlayerStates.PlayerStates.Move, _moveState = new MoveState(this, PlayerStates.PlayerStates.Move.ToString()));
        PlayerStateMachine.States.Add(PlayerStates.PlayerStates.Jump, _jumpState = new JumpState(this, PlayerStates.PlayerStates.Jump.ToString()));
        PlayerStateMachine.Initialize(_idleState);
        
    }

    private void OnGameStateEntered(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.PlayMode:
                break;
            case GameStates.BuildMode:
                MoveBackToStartPosition();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
    }

    private void MoveBackToStartPosition()
    {
        // TEMP
        GlobalPosition = new Vector2(456, 1064);
    }
    
}