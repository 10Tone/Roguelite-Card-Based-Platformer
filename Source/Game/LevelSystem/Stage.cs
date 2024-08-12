using Godot;
using System;
using Game.WorldBuilding;
using Godot.Collections;

namespace LevelSystem;

[GlobalClass]
public partial class Stage : Resource
{
    [Export] public int MinScoreToAdvance{ get; private set; }
    [Export] public int MaxPlaceablePlatforms { get; private set; }
    public int SurplusScore {get; set; }
    public Dictionary<BuildItemResource, int> AvailableBuildItems { get; set; }

}