﻿using Godot;

namespace Game.PlayerStates;

public class IdleState: GroundedState
{
    public IdleState(IPlayer player, string animName) : base(player, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate(float delta)
    {
        base.PhysicsUpdate(delta);
        if (Player.InputHandler.HorizontalInput != 0f)
        {
            Player.PlayerStateMachine.ChangeState(Player.PlayerStateMachine.States[PlayerStates.Move]);
        }

        var motion = Player.Motion;
        motion.x = Mathf.Lerp(motion.x, 0, Player.PlayerData.Friction);
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