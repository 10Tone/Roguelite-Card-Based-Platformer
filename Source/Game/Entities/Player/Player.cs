using AutoLoads;
using Godot;

namespace Game;

public class Player : KinematicBody2D
{
    [Export()] private PlayerData _playerData;
    [Export()] private NodePath _inputHandlerPath;
    
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    
    public PlayerStateMachine PlayerFSM { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _playerData = (PlayerData)GD.Load("res://Source/Game/Entities/Player/Data/PlayerData.tres");
        PlayerFSM = new PlayerStateMachine();
    }

    public override void _Ready()
    {
        InputHandler = GetNode(_inputHandlerPath) as PlayerInputHandler;
    }
    
    public override void _Process(float delta) 
    {
        PlayerFSM.CurrentState.LogicUpdate(delta);
    }

    public override void _PhysicsProcess(float delta)
    {
        PlayerFSM.CurrentState.PhysicsUpdate(delta);
    }
}