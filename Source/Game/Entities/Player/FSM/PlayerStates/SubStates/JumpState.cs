namespace Game.PlayerStates;

public class JumpState: NotGroundedState
{
    public JumpState(IPlayer player, string animName) : base(player, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        var motion = Player.Motion;
        motion.y = -Player.PlayerData.JumpForce;
        Player.Motion = motion;
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