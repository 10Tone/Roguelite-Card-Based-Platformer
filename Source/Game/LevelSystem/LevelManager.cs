using System;
using System.Linq;
using AutoLoads;
using Game.WorldBuilding;
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
	
	[Signal]
	public delegate void LevelFinishedEventHandler();
    
	[Signal]
	public delegate void StageFinishedEventHandler();
    
	[Signal]
	public delegate void PlayerDeathEventHandler();

	public override void _EnterTree()
	{
		_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
		_globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
	}

	public override void _Ready()
	{
		_levelGoal = GetNode<LevelGoal>(_levelGoalPath);
		_levelGoal.LevelGoalReached += OnLevelGoalReached;
		
		// most Idamge intsantiate when block is build
		var damageableChildren = GetTree().GetNodesInGroup("IDamageGroup");
        foreach (var damageable in damageableChildren)
        {
	        DebugOverlay.Instance.DebugPrint(damageable.Name);
	        if (damageable is IDamage iDamage)
	        {
		        iDamage.PlayerEnteredIDamage += OnPlayerEnteredIDamage;
		        
	        }
        }
        
	}

	private void OnLevelGoalReached()
	{
		LoadNextStage();
	}
	

	private void FinalStageFinished()
	{
		
	}

	private void LoadCurrentStage()
	{
		
	}

	private void LoadNextStage()
	{
		// LevelData.CurrentStage.SurplusScore = LevelValue - LevelData.CurrentStage.MinScoreToAdvance
		LevelData.LevelScore += LevelData.CurrentStage.SurplusScore;
		if (LevelData.CurrentStageIndex < LevelData.Stages.Length - 1)
		{
			LevelData.CurrentStageIndex++;
			LevelData.CurrentStage = LevelData.Stages[LevelData.CurrentStageIndex];
			//emit signal with updated game data
		}
		else
		{
			FinalStageFinished();
		}
	}

	private void OnPlayerEnteredIDamage(object sender, EventArgs e)
	{
		DebugOverlay.Instance.DebugPrint("Player entered IDamage");
	}
	
	

	public void UnlockLevel()
	{
		if (!LevelData.IsLocked)
		{
			return;}
		LevelData.IsLocked = false;
	}
}
