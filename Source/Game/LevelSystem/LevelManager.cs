using AutoLoads;
using Godot;
using Tools;

namespace Game.LevelSystem;
public partial class LevelManager : Node
{
	[Export] public LevelData LevelData { get; private set; }
	[Export] private NodePath _levelGoalPath;
	
	private LevelGoal _levelGoal;
	
	private GlobalEvents _globalEvents;
	private GlobalVariables _globalVariables;

	public override void _EnterTree()
	{
		_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
		_globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
	}

	public override void _Ready()
	{
		_levelGoal = GetNode<LevelGoal>(_levelGoalPath);
		_levelGoal.LevelGoalReached += OnLevelGoalReached;
	}

	private void OnLevelGoalReached()
	{
		LoadNextStage();
	}

	private void OnStageFinished()
	{
		LevelData.LevelScore += LevelData.CurrentStage.SurplusScore;
		if (LevelData.CurrentStageIndex < LevelData.Stages.Length - 1)
		{
			LevelData.CurrentStageIndex++;
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
		DebugOverlay.Instance.DebugPrint("Loading next stage...");
	}

	private void OnPlayerDeath()
	{
		
	}
	
	

	public void UnlockLevel()
	{
		if (!LevelData.IsLocked)
		{
			return;}
		LevelData.IsLocked = false;
	}
}
