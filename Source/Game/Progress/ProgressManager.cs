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
		var levelManager = new LevelManager();
		levelManager.LevelData.SetCurrentStageIndex(1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
