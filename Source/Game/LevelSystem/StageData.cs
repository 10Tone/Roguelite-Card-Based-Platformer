using Game.WorldBuilding;
using Godot;
using Godot.Collections;

namespace Game.LevelSystem;

[GlobalClass]
public partial class StageData : Resource
{
    [Export] public int StageNumber { get; private set; }
    [Export] public int MinScoreToAdvance{ get; private set; }
    [Export] public int MaxPlaceablePlatforms { get; private set; }
    public int SurplusScore {get; set; }
    public Dictionary<BuildItemResource, int> AvailableBuildItems { get; set; }

}