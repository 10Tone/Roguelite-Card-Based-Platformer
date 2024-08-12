using Godot;
using System;
using Game.WorldBuilding;
using Godot.Collections;

namespace LevelSystem;

[GlobalClass]
public partial class Stage : Resource
{
    [Export] public int MinScoreToAdvance{ get; private set; }
    [Export] public int SurplusScore{get; private set; }
    [Export] public int MaxPlaceablePlatforms { get; private set; }
    [Export] public Dictionary<BuildItemResource, int> AvailableBuildItems { get; private set; }

}