using Godot;
using System;
using System.Collections.Generic;
using LevelSystem;

public partial class ProgressManager : Node
{
	private List<Level> _unlockedLevels;
	private Level _currentLevel;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
