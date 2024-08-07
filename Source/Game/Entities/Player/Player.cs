using System;
using AutoLoads;
using Game.PlayerStates;
using Godot;
using Tools;

namespace Game;

public partial class Player : CharacterBody2D, IPlayer
{
    [Export()] private PlayerData _playerData;
    [Export()] private NodePath _inputHandlerPath;
    [Export] private NodePath _animatedSprite2DPath;

    public PlayerStateMachine PlayerStateMachine { get; set; }
    public PlayerData PlayerData { get; set; }
    public PlayerInputHandler InputHandler { get; set; }
    public Vector2 Motion { get; set; }
    public bool IsGrounded { get; set; }
    
    public Vector2 PlayerVelocity
    {
        get => Velocity;
        set => Velocity = value;
    }

    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    
    private IdleState _idleState;
    private MoveState _moveState;
    private JumpState _jumpState;
    
    private AnimatedSprite2D _animatedSprite2D;

    public override void _EnterTree()
    {
        PlayerStateMachine = new PlayerStateMachine();
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _playerData = (PlayerData)GD.Load("res://Source/Game/Entities/Player/Data/PlayerData.tres");

        // _globalEvents.Connect(nameof(GlobalEvents.GameStateEnteredEventHandler), new Callable(this, nameof(OnGameStateEntered)));
        _globalEvents.GameStateEntered += OnGameStateEntered;
    }

    public override void _Ready()
    {
        AddToGroup("Player");
        PlayerData = _playerData;
        if(PlayerData is null) {GD.PushWarning("PlayerData is null!");}
        InputHandler = GetNode(_inputHandlerPath) as PlayerInputHandler;
        if(InputHandler is null) {GD.PushWarning("InputHandler is null!");}
        _animatedSprite2D = GetNode(_animatedSprite2DPath) as AnimatedSprite2D;
        AddStates();
    }
    
    public override void _Process(double delta) 
    {
        PlayerStateMachine.CurrentState.LogicUpdate(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        PlayerStateMachine.CurrentState.PhysicsUpdate(delta);
        IsGrounded = IsOnFloor();
        var velocity = Velocity;
        velocity.Y += (float)delta * PlayerData.Gravity;
        Velocity = velocity;
        MoveAndSlide();
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
                InputHandler.InputEnabled = true;
                break;
            case GameStates.BuildMode:
                InputHandler.InputEnabled = false;
                MoveBackToStartPosition();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
    }

    private void MoveBackToStartPosition()
    {
        // TEMP
        GlobalPosition = new Vector2(50, 300);
    }
    
}