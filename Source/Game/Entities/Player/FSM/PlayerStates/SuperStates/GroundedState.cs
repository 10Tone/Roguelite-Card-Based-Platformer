namespace Game.PlayerStates;

public partial class GroundedState: PlayerState
{
    public GroundedState(IPlayer player, string animName) : base(player, animName)
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
        if (Player.InputHandler.JumpInput && Player.IsGrounded)
        {
            Player.PlayerStateMachine.ChangeState(Player.PlayerStateMachine.States[PlayerStates.Jump]);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }


}