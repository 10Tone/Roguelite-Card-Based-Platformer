using Godot;
using System;
using Tools;

namespace Game.WorldBuilding;

public partial class Trap : Node2D, IBuildItem, IDamage
{
	[Export] protected NodePath _area2DPath;
	[Export] public string ResourcePath { get; set; }
	
	protected BuildItemResource _buildItemResource;
	protected bool _trapActive;
	protected bool _playerIsInTrap;

	public event EventHandler IDamageActive;

	BuildItemResource IBuildItem.BuildItemResource
	{
		get => _buildItemResource;
		set => _buildItemResource = value;
	}
	BuildItemResource BuildItemResource { get; set; }

	protected Area2D _area2D;
	public bool PlayerEnteredIDamage { get; set; }

	public override void _EnterTree()
	{
		_area2D = GetNode<Area2D>(_area2DPath);
		_area2D.BodyEntered += OnPlayerEntered;
		_area2D.BodyExited += OnPlayerExited;
		AddToIDamageGroup();
		_trapActive = true;
	}

	public override void _Ready()
	{
		_buildItemResource = GD.Load<BuildItemResource>(ResourcePath);
	}

	protected void OnPlayerEntered(Node2D player)
	{
		PlayerEnteredIDamage = true;
		if (player is not IPlayer iplayer) return;
		// DebugOverlay.Instance.DebugPrint("Player entered");

		// if (_trapActive)
		// {

		// 	IDamageActive?.Invoke(this, EventArgs.Empty);
		// }
		

	}

	private void OnPlayerExited(Node2D player)
	{
		_playerIsInTrap = false;
		// DebugOverlay.Instance.DebugPrint("Player exited");
		PlayerEnteredIDamage = false;
	}

	public void AddToIDamageGroup()
	{
		DebugOverlay.Instance.DebugPrint("Adding to IDamageGroup");
		AddToGroup("IDamageGroup");
		
	}

	public int GetIDamageValue()
	{
		return _buildItemResource.DamageValue;
	}

	public bool GetTrapActive()
	{
		return _trapActive;
	}




	protected void TrapStateChanged()
	{
		if (!_trapActive) return;
		IDamageActive?.Invoke(this, EventArgs.Empty);
	}
	
	
}
