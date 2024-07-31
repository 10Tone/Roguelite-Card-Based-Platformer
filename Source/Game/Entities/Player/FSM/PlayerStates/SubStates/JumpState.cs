namespace Game.PlayerStates;

public partial class JumpState: NotGroundedState
{
    public JumpState(IPlayer player, string animName) : base(player, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        var velocity = Player.PlayerVelocity;
        velocity.Y = -Player.PlayerData.JumpForce;
        Player.PlayerVelocity = velocity;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate(double delta)
    {
        base.LogicUpdate(delta);
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}