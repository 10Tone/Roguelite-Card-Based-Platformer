using AutoLoads;
using Godot;
using Tools;

namespace Game;

public partial class BuildModeState : GameState
{
    // Add a parameterless constructor
    public BuildModeState() : base(null, null)
    {
    }

    public BuildModeState(GlobalEvents globalEvents, GlobalVariables globalVariables) : base(globalEvents, globalVariables)
    {
    }

    public override void Enter()
    {
        base.Enter();
        DebugOverlay.Instance.DebugPrint("BuildMode entered");
        GlobalEvents.EmitSignal(nameof(GlobalEvents.GameStateEntered), (int)GameStates.BuildMode);
    }

    // Remove redundant overrides
    // public override void Exit()
    // {
    //     base.Exit();
    // }

    // public override void LogicUpdate(double delta)
    // {
    //     base.LogicUpdate(delta);
    // }

    // public override void PhysicsUpdate(double delta)
    // {
    //     base.PhysicsUpdate(delta);
    // }

    // public override void DoChecks()
    // {
    //     base.DoChecks();
    // }
}