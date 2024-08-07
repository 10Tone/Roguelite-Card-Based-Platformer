using Godot;

namespace Game.WorldBuilding;

public interface IBuildItem
{
    [Export] protected string ResourcePath { get; set; }
    public BuildItemResource BuildItemResource { get; set; }
}