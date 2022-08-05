using AutoLoads;
using Godot;

namespace Game.WorldBuilding.Platforms;

public class Platform : StaticBody2D, IBuildItem
{
    [Export()] public int BuildItemValue { get; set; }
    [Export()] public BuildItemTypes BuildItemType { get; set; }
}