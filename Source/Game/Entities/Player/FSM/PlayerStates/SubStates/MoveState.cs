namespace Game.PlayerStates;

public class MoveState: GroundedState
{
    public MoveState(IPlayer player, string animName) : base(player, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate(float delta)
    {
        base.PhysicsUpdate(delta);
        if (Player.InputHandler.HorizontalInput == 0f)
        {
            Player.PlayerStateMachine.ChangeState(Player.PlayerStateMachine.States[PlayerStates.IDLE]);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate(float delta)
    {
        base.LogicUpdate(delta);
    }
}