using Godot;

namespace Game.LevelSystem;
[GlobalClass]
public partial class LevelData : Resource
{
	[Export] public string LevelName { get; private set; }
    [Export] public int LevelNumber { get; private set; }
    [Export] public bool IsLocked { get; private set; }
	[Export] public Game.LevelSystem.StageData[] Stages { get; private set; }
	public Game.LevelSystem.StageData CurrentStage { get; set; }
	public int CurrentStageIndex { get; private set; }
	public Game.LevelSystem.StageData[] CompletedStages { get; set; }
	public int LevelScore { get; set; }
	
	internal void SetCurrentStageIndex(int index)
	{
        CurrentStageIndex = index;
    }
}
 