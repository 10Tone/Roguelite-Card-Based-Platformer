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
    
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    
    private IdleState _idleState;
    private MoveState _moveState;

    public override void _EnterTree()
    {
        PlayerStateMachine = new PlayerStateMachine();
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _playerData = (PlayerData)GD.Load("res://Source/Game/Entities/Player/Data/PlayerData.tres");
    }

    public override void _Ready()
    {
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
       
    }

    private void AddStates()
    {
        PlayerStateMachine.States.Add(PlayerStates.PlayerStates.Idle, _idleState = new IdleState(this, PlayerStates.PlayerStates.Idle.ToString()));
        PlayerStateMachine.States.Add(PlayerStates.PlayerStates.Move, _moveState = new MoveState(this, PlayerStates.PlayerStates.Move.ToString()));
        PlayerStateMachine.Initialize(_idleState);
        
    }
    
}