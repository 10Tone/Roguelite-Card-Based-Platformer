using AutoLoads;
using Tools;

namespace Game;

public partial class DeathState: GameState
{
    public DeathState(GlobalEvents globalEvents, GlobalVariables globalVariables) : base(globalEvents, globalVariables){}

    public DeathState() : base(null, null){}

    public override void Enter()
    {
        base.Enter();
        DebugOverlay.Instance.DebugPrint("Death state entered");
    }
}
