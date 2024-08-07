using Godot;
using System;
using AutoLoads;

public partial class LevelPointsDisplay : Control
{
	[Export] private NodePath _levelPointsLabelNodePath;
	
	private Label _levelPointsLabel;
	private GlobalEvents _globalEvents;
	

	public override void _Ready()
	{
		_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
		_levelPointsLabel = GetNode<Label>(_levelPointsLabelNodePath);
		_globalEvents.LevelValueUpdated += OnLevelValueUpdated;
	}

	private void OnLevelValueUpdated(int value)
	{
		if(_levelPointsLabel == null) return;
		
		_levelPointsLabel.Text = value.ToString();
	}
}
