using Godot;

namespace Game.WorldBuilding;

public interface IBuildItem
{
    public int BuildItemValue { get; set; }
    public BuildItemTypes BuildItemType { get; set; }
}