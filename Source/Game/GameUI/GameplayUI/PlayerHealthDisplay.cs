using Godot;
using System;
using AutoLoads;

public partial class PlayerHealthDisplay : Control
{
	[Export] NodePath _healthLabelPath;
	
	private Label _healthLabel;
	private GlobalEvents _globalEvents;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_healthLabel = GetNode<Label>(_healthLabelPath);
	}

	public override void _EnterTree()
	{
		_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
		_globalEvents.PlayerHealthUpdated += OnPlayerHealthUpdated;
	}

	private void OnPlayerHealthUpdated(int health, bool isdead)
	{
		_healthLabel.Text = health.ToString();
	}
}
