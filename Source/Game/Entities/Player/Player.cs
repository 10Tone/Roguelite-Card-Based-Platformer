using System;
using AutoLoads;
using Game.PlayerStates;
using Game.WorldBuilding;
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
    public int FacingDirection { get; private set; }
    public AnimatedSprite2D AnimatedSprite { get; set; }

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
    private Area2D _iDamageCollider;
    private bool _isDamaged = false;
    private int _health;

    #region Initialization
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
        _globalVariables.StartPosition = GlobalPosition;
        AddToGroup("Player");
        PlayerData = _playerData;
        if (PlayerData is null)
        {
            GD.PushWarning("PlayerData is null!");
        }
        ResetHealth();
        InputHandler = GetNode(_inputHandlerPath) as PlayerInputHandler;
        if (InputHandler is null)
        {
            GD.PushWarning("InputHandler is null!");
        }

        _animatedSprite2D = GetNode(_animatedSprite2DPath) as AnimatedSprite2D;
        FacingDirection = 1;
        AnimatedSprite = _animatedSprite2D;
        _iDamageCollider = GetNode<Area2D>("IDamageCollider");
        _iDamageCollider.AreaEntered += OnPlayerEnteredIDamage;
        _iDamageCollider.AreaExited += OnPlayerExitedIDamage;
        AddStates();
    }

    private void AddStates()
    {
        PlayerStateMachine.States.Add(PlayerStates.PlayerStates.Idle,
            _idleState = new IdleState(this, PlayerStates.PlayerStates.Idle.ToString()));
        PlayerStateMachine.States.Add(PlayerStates.PlayerStates.Move,
            _moveState = new MoveState(this, PlayerStates.PlayerStates.Move.ToString()));
        PlayerStateMachine.States.Add(PlayerStates.PlayerStates.Jump,
            _jumpState = new JumpState(this, PlayerStates.PlayerStates.Jump.ToString()));
        PlayerStateMachine.Initialize(_idleState);
    }

    #endregion

    public override void _Process(double delta)
    {
        PlayerStateMachine.CurrentState.LogicUpdate(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!InputHandler.InputEnabled)
        {
            return;
        }

        PlayerStateMachine.CurrentState.PhysicsUpdate(delta);
        IsGrounded = IsOnFloor();
        var velocity = Velocity;
        velocity.Y += (float)delta * PlayerData.Gravity;
        Velocity = velocity;
        MoveAndSlide();
    }

    #region Event Handlers

    private void OnGameStateEntered(GameState gameState)
    {
        if (gameState == _globalVariables.GameStates[GameModeState.PlayModeState])
        {
            InputHandler.InputEnabled = true;
        }
        else
        {
            InputHandler.InputEnabled = false;
            PlayerStateMachine.ChangeState(_idleState);
            MoveBackToStartPosition();
        }
    }

    private void OnPlayerEnteredIDamage(Area2D area)
    {
        if (_isDamaged)
        {
            return;}
        if (area is not IDamage damageable)
        {
            damageable = area.GetParent() as IDamage;
        }

        if (damageable == null)
        {
            var parent = area.GetParent();
            if (parent != null)
            {
                foreach (var child in parent.GetChildren())
                {
                    if (child is IDamage damage)
                    {
                        damageable = damage;
                        break;
                    }
                }
            }
        }
        
        var node = damageable as Node2D;
        if (damageable == null || node == null) return;
        if (!node.IsInGroup("IDamageGroup"))
        {
            return;}

        _isDamaged = true;
        DealDamage(damageable.GetIDamageValue());
        DebugOverlay.Instance.DebugPrint("Player health: " + _health);
    }

    private void OnPlayerExitedIDamage(Area2D area)
    {
        _isDamaged = false;
    }

    #endregion

    #region Movement and Animation

    private void MoveBackToStartPosition()
    {
        // TEMP
        GlobalPosition = _globalVariables.StartPosition;
    }

    private void Flip()
    {
        FacingDirection *= -1;
        _animatedSprite2D.FlipH = FacingDirection switch
        {
            1 => false,
            -1 => true,
            _ => _animatedSprite2D.FlipH
        };
    }

    public void CheckIfShouldFlip(float horizontalInput)
    {
        if (horizontalInput != 0 && Convert.ToInt32(horizontalInput) != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    private void DealDamage(int damage)
    {
        _health -= damage;
        
        if (_health <= 0)
        {
            Die();
        }
        _globalEvents.EmitSignal(nameof(_globalEvents.PlayerHealthUpdated), _health, false);
    }

    private void Die()
    {
        _globalEvents.EmitSignal(nameof(_globalEvents.PlayerHealthUpdated), 0, true);
    }

    private void ResetHealth()
    {
        _health = PlayerData.Health;
    }
}