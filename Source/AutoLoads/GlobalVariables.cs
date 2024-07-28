using Game;
using Game.WorldBuilding;
using Godot;

namespace AutoLoads;

public partial class GlobalVariables: Node
{
    public bool PlayerInputEnabled { get; set; }
    public BuildItemResource SelectedBuildItem { get; set; }
    public GameStates GameState { get; set; }
}