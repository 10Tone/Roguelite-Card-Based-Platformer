using Godot;

namespace Game.WorldBuilding;

public interface IBuildItem
{
    // [Export] public int BuildItemValue { get; protected set; }
    // [Export] public BuildItemTypes BuildItemType { get; protected set; }
    [Export] public Resource BuildItemResource { get; protected set; }
}