using AutoLoads;
using Tools;

namespace Game;

public partial class DeathModeState: GameState
{
    public DeathModeState(GlobalEvents globalEvents, GlobalVariables globalVariables) : base(globalEvents,
        globalVariables)
    {
            _globalVariables = globalVariables;
    }

    public DeathModeState() : base(null, null){}
    
    private GlobalVariables _globalVariables;

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

        
        
        foreach (var buildItem in _globalVariables.BuildItemResources)
        {
            buildItem.ResetToInitialState();
        }
    }
}
