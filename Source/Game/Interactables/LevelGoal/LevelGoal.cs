using Godot;
using System;
using AutoLoads;
using Game;

public partial class LevelGoal : Interactable
{
    [Signal] public delegate void LevelGoalReachedEventHandler();
    protected override void BodyEnteredAction(IPlayer player)
    {
        base.BodyEnteredAction(player);
        EmitSignal(nameof(LevelGoalReached));
    }
}
