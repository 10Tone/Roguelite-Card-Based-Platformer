using Godot;
using System;
using Game;

public partial class DeadBox : Interactable
{
	// Called when the node enters the scene tree for the first time.
	protected override void BodyEnteredAction(IPlayer player)
	{
		base.BodyEnteredAction(player);
		GlobalEvents.EmitSignal(nameof(GlobalEvents.PlayerDeath));
	}
}
