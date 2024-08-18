using System;
using System.Linq;
using AutoLoads;
using Game.WorldBuilding;
using Godot;
using Godot.Collections;
using Tools;

namespace Game.LevelSystem;
public partial class LevelManager : Node
{
	[Export] public LevelData LevelData { get; private set; }
	[Export] private NodePath _levelGoalPath, _worldManagerPath;
	
	private LevelGoal _levelGoal;
	private WorldManager _worldManager;
	
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
		_worldManager = GetNode<WorldManager>(_worldManagerPath);
		
		_levelGoal.LevelGoalReached += OnLevelGoalReached;
		
		_globalEvents.ItemBuild += OnItemBuild;
		_globalEvents.ItemRemoved += OnItemRemoved;
		
		LevelData.CurrentStage = LevelData.Stages[LevelData.CurrentStageIndex];
		ScanForDamageableNodes();
		
	}

	private void ScanForDamageableNodes()
	{
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

	private void OnItemBuild(BuildItemResource builditemresource, Node2D item, Dictionary<Vector2, Node2D> neighbors)
	{
		// check if item implements IDamage interface, if so connect to PlayerEneteredIDamage event
		
		if (item is IDamage iDamage)
        {
            iDamage.PlayerEnteredIDamage += OnPlayerEnteredIDamage;
        }
	}

	private void OnItemRemoved(BuildItemResource builditemresource, Node2D item, Dictionary<Vector2, Node2D> neighbors)
	{
		if (item is IDamage iDamage)
		{
			iDamage.PlayerEnteredIDamage -= OnPlayerEnteredIDamage;
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
		_worldManager?.OnStageFinished();
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
