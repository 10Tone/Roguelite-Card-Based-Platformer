using Game;
using Game.WorldBuilding;
using Godot;

namespace AutoLoads;

public class GlobalEvents : Node
{
    [Signal]
    public delegate void GameReady();

    [Signal]
    public delegate void BuildItemButtonClicked(BuildItemResource buildItemResource);
}