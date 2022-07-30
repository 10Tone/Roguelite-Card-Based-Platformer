using AutoLoads;
using Game.PlayerStates;
using Godot;
using Tools;

namespace Game;

public class Player : KinematicBody2D, IPlayer
{
    [Export()] private PlayerData _playerData;
    [Export()] private NodePath _inputHandlerPath;
    
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;

    public PlayerStateMachine PlayerStateMachine { get; set; }
    public PlayerData PlayerData { get; set; }
    public PlayerInputHandler InputHandler { get; set; }

    // private PlayerStates.PlayerStates _playerState;
    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }

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
        InputHandler = GetNode(_inputHandlerPath) as PlayerInputHandler;
       
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
        // IdleState = new IdleState(this, "idle");
        // MoveState = new MoveState(this, "move");
        PlayerStateMachine.States.Add(PlayerStates.PlayerStates.IDLE, IdleState = new IdleState(this, "idle"));
        PlayerStateMachine.States.Add(PlayerStates.PlayerStates.MOVE, MoveState = new MoveState(this, "move"));
        PlayerStateMachine.Initialize(IdleState);
        
    
    }
    
}