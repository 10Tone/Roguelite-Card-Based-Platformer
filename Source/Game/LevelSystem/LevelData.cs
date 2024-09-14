using System;
using Godot;

namespace Game.LevelSystem;
[GlobalClass]
public partial class LevelData : Resource
{
	[Export] public string LevelName { get; private set; }
    [Export] public int LevelNumber { get; private set; }
    [Export] public bool IsLocked { get; set; }
	[Export] public StageData[] Stages { get; private set; }
	public StageData CurrentStage { get; set; }
	public int CurrentStageIndex { get; set; }
	public StageData[] CompletedStages { get; set; } = Array.Empty<StageData>();

	public void ResetLevelData()
	{
		CurrentStageIndex = 0;
		CurrentStage = Stages[CurrentStageIndex];
		CompletedStages = Array.Empty<StageData>();
		IsLocked = true;
	}
	
}
 