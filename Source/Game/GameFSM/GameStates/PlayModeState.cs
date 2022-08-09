using AutoLoads;
using Godot;

namespace Game;

public class PlayModeState: GameState
{
    public PlayModeState(GlobalEvents globalEvents, GlobalVariables globalVariables) : base(globalEvents, globalVariables)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GD.Print("BuildMode entered");
        GlobalVariables.PlayerInputEnabled = true;
        GlobalEvents.EmitSignal(nameof(GlobalEvents.GameStateEntered), GameStates.PlayMode);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate(float delta)
    {
        base.LogicUpdate(delta);
    }

    public override void PhysicsUpdate(float delta)
    {
        base.PhysicsUpdate(delta);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}