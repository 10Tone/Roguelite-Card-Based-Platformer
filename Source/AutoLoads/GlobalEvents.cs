using Game;
using Game.WorldBuilding;
using Godot;
using Godot.Collections;

namespace AutoLoads;

public partial class GlobalEvents : Node
{
    [Signal]
    public delegate void GameReadyEventHandler();

    [Signal]
    public delegate void BuildItemButtonClickedEventHandler(BuildItemResource buildItemResource);

    [Signal]
    public delegate void ItemBuildEventHandler(BuildItemResource buildItemResource, Dictionary<Vector2, Node2D> neighbors);

    [Signal]
    public delegate void ItemRemovedEventHandler(BuildItemResource buildItemResource, Dictionary<Vector2, Node2D> neighbors);

    [Signal]
    public delegate void LevelValueUpdatedEventHandler(int value);

    [Signal]
    public delegate void GameStateEnteredEventHandler(GameStates gameState);

    [Signal]
    public delegate void GameModeButtonPressedEventHandler();

    [Signal]
    public delegate void PlayerFinishedLevelEventHandler();
}