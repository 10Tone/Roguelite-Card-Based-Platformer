using Godot;
using System;

namespace LevelSystem;
public partial class Level : Node2D
{
	[Export] private Stage[] _stages;
	private Stage _currentStage;
	private int _currentStageIndex;
	public Stage[] CompletedStages { get; private set; }
	public int LevelScore { get; private set; }

	public override void _Ready()
	{
		_currentStage = _stages[_currentStageIndex];
	}

	private void OnStageFinished()
	{
		LevelScore += _currentStage.SurplusScore;
		if (_currentStageIndex < _stages.Length - 1)
		{
			_currentStageIndex++;
		}
		else
		{
			FinalStageFinished();
		}
		
	}

	private void FinalStageFinished()
	{
		
	}

	private void LoadCurrentStage()
	{
		
	}

	private void LoadNextStage()
	{
		
	}

	private void OnPlayerDeath()
	{
		
	}
	
}
