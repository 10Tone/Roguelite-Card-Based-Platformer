using Godot;
using System;

namespace Game.WorldBuilding;

public partial class Trap : Node2D, IBuildItem, IDamage
{
	[Export] protected NodePath _area2DPath;
	[Export] public Resource BuildItemResource { get; set; }
	// [Export] public int BuildItemValue { get; private set; }
	// int IBuildItem.BuildItemValue
	// {
	// 	get => BuildItemValue;
	// 	set => BuildItemValue = value;
	// }
	//
	// [Export] public BuildItemTypes BuildItemType { get; private set; }
	// [Export] public Resource BuildItemResource { get; set; }
	//
	// BuildItemTypes IBuildItem.BuildItemType
	// {
	// 	get => BuildItemType;
	// 	set => BuildItemType = value;
	// }

	protected Area2D _area2D;

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
