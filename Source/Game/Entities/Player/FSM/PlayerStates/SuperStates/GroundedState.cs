namespace Game.PlayerStates;

public class GroundedState: PlayerState
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