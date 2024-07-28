namespace Game.PlayerStates;

public partial class NotGroundedState: PlayerState
{
    public NotGroundedState(IPlayer player, string animName) : base(player, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
        if (Player.IsGrounded)
        {
            Player.PlayerStateMachine.ChangeState(Player.PlayerStateMachine.States[PlayerStates.Idle]);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}