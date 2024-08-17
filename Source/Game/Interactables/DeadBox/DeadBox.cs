using Godot;
using System;
using Game;
using Game.WorldBuilding;
using Tools;

public partial class DeadBox : Interactable, IDamage
{
	public event EventHandler PlayerEnteredIDamage;

	public override void _EnterTree()
	{
		base._EnterTree();
		AddToIDamageGroup();
	}

	public void AddToIDamageGroup()
	{
		AddToGroup("IDamageGroup");
	}

	protected override void BodyEnteredAction(IPlayer player)
	{
		base.BodyEnteredAction(player);
		
		PlayerEnteredIDamage?.Invoke(this, EventArgs.Empty);
	}


}
