using Game;
using Game.WorldBuilding;
using Godot;

namespace AutoLoads;

public partial class GlobalVariables: Node
{
    public BuildItemResource SelectedBuildItem { get; set; }
    public GameStates GameState { get; set; }
    public Vector2I WorldGridSize { get; set; }
}