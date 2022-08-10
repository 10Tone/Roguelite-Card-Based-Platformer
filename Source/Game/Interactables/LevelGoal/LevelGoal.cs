using Godot;
using System;
using AutoLoads;
using Game;

public class LevelGoal : Interactable
{
    protected override void BodyEnteredAction(IPlayer player)
    {
        base.BodyEnteredAction(player);
        GlobalEvents.EmitSignal(nameof(GlobalEvents.PlayerFinishedLevel));
    }
}
