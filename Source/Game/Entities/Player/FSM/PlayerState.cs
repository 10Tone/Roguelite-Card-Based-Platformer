using Godot;
using Tools;

namespace Game;

public class PlayerState:State
{
    protected IPlayer Player { get;}

    protected string AnimName;

    protected PlayerState(IPlayer player, string animName)
    {
        Player = player;
        this.AnimName = animName;
    }

    public override void Enter()
    {
        base.Enter();
        GD.Print( AnimName + " state entered");
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