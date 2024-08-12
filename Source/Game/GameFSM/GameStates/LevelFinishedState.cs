using AutoLoads;
using Tools;

namespace Game;

public partial class LevelFinishedState: GameState
{
    public LevelFinishedState(GlobalEvents globalEvents, GlobalVariables globalVariables) : base(globalEvents, globalVariables){}
    public LevelFinishedState() : base(null, null){}

    public override void Enter()
    {
        base.Enter();
        DebugOverlay.Instance.DebugPrint("Level finished state entered");
    }
}
