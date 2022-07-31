using AutoLoads;
using Godot;

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
            Player.PlayerStateMachine.ChangeState(Player.PlayerStateMachine.States[PlayerStates.Idle]);
        }
        Move(delta);
    }
    
    private void Move(float delta)
    {
        var motion = Player.Motion;
        motion.x += Player.InputHandler.HorizontalInput * Player.PlayerData.Acceleration;
        motion.x = Mathf.Clamp(motion.x, -Player.PlayerData.MaxSpeed, Player.PlayerData.MaxSpeed);
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

    public override void LogicUpdate(float delta)
    {
        base.LogicUpdate(delta);
    }
}