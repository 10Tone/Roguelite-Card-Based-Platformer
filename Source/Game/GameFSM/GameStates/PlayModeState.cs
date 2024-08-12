using AutoLoads;
using Godot;
using Tools;

namespace Game;

public partial class PlayModeState: GameState
{
    public PlayModeState() : base(null, null)
    {
    }
    public PlayModeState(GlobalEvents globalEvents, GlobalVariables globalVariables) : base(globalEvents, globalVariables)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (GlobalEvents != null)
        {
            GlobalEvents.EmitSignal(nameof(GlobalEvents.GameStateEntered), this);
        }
        if (DebugOverlay.Instance != null)
        {
            DebugOverlay.Instance.DebugPrint(GetType().Name + " entered");
        }
        
    }

    // public override void Exit()
    // {
    //     base.Exit();
    // }
    //
    // public override void LogicUpdate(double delta)
    // {
    //     base.LogicUpdate(delta);
    // }
    //
    // public override void PhysicsUpdate(double delta)
    // {
    //     base.PhysicsUpdate(delta);
    // }
    //
    // public override void DoChecks()
    // {
    //     base.DoChecks();
    // }
}