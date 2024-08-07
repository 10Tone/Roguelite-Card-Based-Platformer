using Godot;
using System;

namespace Game.WorldBuilding;

public partial class Trap : Node2D, IBuildItem, IDamage
{
	[Export] protected NodePath _area2DPath;
	[Export] public string ResourcePath { get; set; }
	
	protected BuildItemResource _buildItemResource;
	
	BuildItemResource IBuildItem.BuildItemResource
	{
		get => _buildItemResource;
		set => _buildItemResource = value;
	}

	BuildItemResource BuildItemResource { get; set; }

	protected Area2D _area2D;
	

	public override void _EnterTree()
	{
		_area2D = GetNode<Area2D>(_area2DPath);
		_area2D.BodyEntered += OnPlayerEntered;
	}
	
	public override void _Ready()
	{
		_buildItemResource = GD.Load<BuildItemResource>(ResourcePath);
	}

	public void OnPlayerEntered(Node2D player)
	{
		GD.Print("Player entered trap!");
		
	}

	
}
