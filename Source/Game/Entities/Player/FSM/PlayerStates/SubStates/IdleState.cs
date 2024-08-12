using Godot;

namespace Game.PlayerStates;

public partial class IdleState: GroundedState
{
    public IdleState(IPlayer player, string animName) : base(player, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        if (Player.InputHandler.HorizontalInput != 0f)
        {
            Player.PlayerStateMachine.ChangeState(Player.PlayerStateMachine.States[PlayerStates.Move]);
        }

        var velocity = Player.PlayerVelocity;
        velocity.X = Mathf.Lerp(velocity.X, 0, Player.PlayerData.Friction);
        Player.PlayerVelocity = velocity;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate(double delta)
    {
        base.LogicUpdate(delta);
        
    }


}