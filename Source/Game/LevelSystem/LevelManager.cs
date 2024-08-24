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
	[Export] private NodePath _levelGoalPath, _worldManagerPath, _levelValueCalculatorPath;
	
	private LevelGoal _levelGoal;
	private WorldManager _worldManager;
	private LevelValueCalculator _levelValueCalculator;
	
	private GlobalEvents _globalEvents;
	private GlobalVariables _globalVariables;
	
	private bool _minimumStageValueReached = false;
	
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
		_levelValueCalculator = GetNode<LevelValueCalculator>(_levelValueCalculatorPath);
		
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
			// DebugOverlay.Instance.DebugPrint(damageable.Name);
			if (damageable is IDamage iDamage)
			{
				iDamage.PlayerEnteredIDamage += OnPlayerEnteredIDamage;
		        
			}
		}
	}

	private void OnItemBuild(BuildItemResource builditemresource, Node2D item, Dictionary<Vector2, Node2D> neighbors)
	{
		_levelValueCalculator.OnItemBuild(LevelData.CurrentStage);
		_minimumStageValueReached = MinimumStageValueReached();
		
		if (item is IDamage iDamage)
        {
            iDamage.PlayerEnteredIDamage += OnPlayerEnteredIDamage;
        }
	}

	private void OnItemRemoved(BuildItemResource builditemresource, Node2D item, Dictionary<Vector2, Node2D> neighbors)
	{
		_levelValueCalculator.OnItemRemoved(LevelData.CurrentStage);
		_minimumStageValueReached = MinimumStageValueReached();
		
		if (item is IDamage iDamage)
		{
			iDamage.PlayerEnteredIDamage -= OnPlayerEnteredIDamage;
		}
	}

	private bool MinimumStageValueReached()
	{
		return _levelValueCalculator.CurrentStageValue >= LevelData.CurrentStage.MinScoreToAdvance;
	}

	private void OnLevelGoalReached()
	{
		if(!_minimumStageValueReached) {return;}
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
		// DebugOverlay.Instance.DebugPrint("Player entered IDamage");
	}
	
	

	public void UnlockLevel()
	{
		if (!LevelData.IsLocked)
		{
			return;}
		LevelData.IsLocked = false;
	}
}
