using Godot;
using System;
using Game.WorldBuilding;

public partial class Trap : Node2D, IBuildItem, IDamage
{
	[Export] private NodePath _area2DPath;
	
	public int BuildItemValue { get; set; }
	public BuildItemTypes BuildItemType { get; set; }
	
	private Area2D _area2D;

	public override void _EnterTree()
	{
		_area2D = GetNode<Area2D>(_area2DPath);
		_area2D.BodyEntered += OnPlayerEntered;
	}

	public void OnPlayerEntered(Node2D player)
	{
		GD.Print("Player entered trap!");
		
	}
}
