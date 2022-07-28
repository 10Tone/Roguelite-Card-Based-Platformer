using Tools;

namespace Game;

public class PlayerState:State
{
    protected Player player;
    protected PlayerStateMachine playerFSM;
    protected PlayerData playerData;
    protected string animName;

    public PlayerState(Player player, PlayerStateMachine playerFSM, PlayerData playerData, string animName)
    {
        this.player = player;
        this.playerFSM = playerFSM;
        this.playerData = playerData;
        this.animName = animName;
    }

    public override void Enter()
    {
        base.Enter();
        // GD.Print(animName);
        // player.AnimationFinished = false;
        // player.Animator.Play(animName);
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