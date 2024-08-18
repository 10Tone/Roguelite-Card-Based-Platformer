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
	public int CurrentStageIndex = 0;
	public StageData[] CompletedStages = Array.Empty<StageData>();
	public int LevelScore = 0;

}
 