using AutoLoads;
using Game;
using Tools;

public partial class StageFinishedModeState: GameState
{
    public StageFinishedModeState(GlobalEvents globalEvents, GlobalVariables globalVariables) : base(globalEvents,
        globalVariables){}

    public StageFinishedModeState() : base(null, null){}
    
    public override void Enter()
    {
        base.Enter();
        GlobalEvents?.EmitSignal(nameof(GlobalEvents.GameStateEntered), this);
        if (DebugOverlay.Instance != null)
        {
            DebugOverlay.Instance.DebugPrint(GetType().Name + " entered");
        }
    }

}
