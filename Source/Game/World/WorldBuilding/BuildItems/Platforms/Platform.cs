using Godot;

namespace Game.WorldBuilding;

public partial class Platform : StaticBody2D, IBuildItem
{
    // [Export()] public int BuildItemValue { get; set; }
    // [Export()] public BuildItemTypes BuildItemType { get; set; }
    [Export] public Resource BuildItemResource { get; set; }
}