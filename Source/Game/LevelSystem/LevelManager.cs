using Godot;

namespace Game.LevelSystem;
public partial class LevelManager : Node
{
	[Export] public LevelData LevelData { get; private set; }

	public override void _Ready()
	{
		
	}

	private void OnStageFinished()
	{
		LevelData.LevelScore += LevelData.CurrentStage.SurplusScore;
		if (LevelData.CurrentStageIndex < LevelData.Stages.Length - 1)
		{
			LevelData.SetCurrentStageIndex(LevelData.CurrentStageIndex + 1);
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
