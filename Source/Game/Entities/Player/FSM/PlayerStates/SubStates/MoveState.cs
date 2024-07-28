using AutoLoads;
using Godot;

namespace Game.PlayerStates;

public partial class MoveState: GroundedState
{
    public MoveState(IPlayer player, string animName) : base(player, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        if (Player.InputHandler.HorizontalInput == 0f)
        {
            Player.PlayerStateMachine.ChangeState(Player.PlayerStateMachine.States[PlayerStates.Idle]);
        }
        Move(delta);
    }
    
    private void Move(double delta)
    {
        var motion = Player.Motion;
        motion.X += Player.InputHandler.HorizontalInput * Player.PlayerData.Acceleration;
        motion.X = Mathf.Clamp(motion.X, -Player.PlayerData.MaxSpeed, Player.PlayerData.MaxSpeed);
        Player.Motion = motion;
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