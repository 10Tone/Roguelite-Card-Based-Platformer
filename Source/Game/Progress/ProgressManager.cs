using Godot;
using System;
using System.Collections.Generic;
using AutoLoads;
using Game.LevelSystem;

public partial class ProgressManager : Node
{
	private List<LevelData> _unlockedLevels;
	private LevelData _currentLevelData;
	
	private GlobalEvents _globalEvents;
	private GlobalVariables _globalVariables;

	public override void _EnterTree()
	{
		_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
		_globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
	}

	public void OnInitializeGame(LevelData levelData)
	{
		_unlockedLevels = new List<LevelData> { levelData };
	}

	public void OnStageFinished(LevelData levelData)
	{
		
	}

	public void OnLevelFinished(LevelData levelData)
	{
		_unlockedLevels.Add(levelData);
	}
	

	public LevelData GetCurrentLevelData()
	{
		return _currentLevelData;
	}

	public void SaveGame()
	{
		
	}
	
}
