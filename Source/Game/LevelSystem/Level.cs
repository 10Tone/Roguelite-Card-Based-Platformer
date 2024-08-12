using Godot;
using System;

namespace LevelSystem;
public partial class Level : Node2D
{
	[Export] private Stage[] _stages;
	
	private Stage _currentStage;
	public int LevelScore { get; private set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_currentStage = _stages[0];
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnStageFinished()
	{
		LevelScore += _currentStage.SurplusScore;
        
        
	}
}
