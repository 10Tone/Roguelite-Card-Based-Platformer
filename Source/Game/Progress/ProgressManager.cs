using Godot;
using System;
using System.Collections.Generic;
using Game.LevelSystem;

public partial class ProgressManager : Node
{
	private List<LevelData> _unlockedLevels;
	private LevelData _currentLevelData;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
