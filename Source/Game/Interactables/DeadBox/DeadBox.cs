using Godot;
using System;
using Game;
using Game.WorldBuilding;
using Tools;

public partial class DeadBox : Interactable, IDamage
{
	public event EventHandler IDamageActive;

	public override void _EnterTree()
	{
		base._EnterTree();
		AddToIDamageGroup();
	}

	public void AddToIDamageGroup()
	{
		AddToGroup("IDamageGroup");
	}

	public int GetIDamageValue()
	{
		return 10000;
	}

	public bool GetIDamageActive()
	{
		return true;
	}

	public bool PlayerEnteredIDamage { get; set; }

	protected override void BodyEnteredAction(IPlayer player)
	{
		base.BodyEnteredAction(player);
		
		IDamageActive?.Invoke(this, EventArgs.Empty);
	}


}
