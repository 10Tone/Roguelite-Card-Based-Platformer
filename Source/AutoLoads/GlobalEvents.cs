using Game;
using Game.LevelSystem;
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
    public delegate void ItemBuildEventHandler(BuildItemResource buildItemResource, Node2D item, Dictionary<Vector2, Node2D> neighbors);

    [Signal]
    public delegate void ItemRemovedEventHandler(BuildItemResource buildItemResource, Node2D item, Dictionary<Vector2, Node2D> neighbors);

    [Signal]
    public delegate void LevelValueUpdatedEventHandler(int value);
    
    [Signal]
    public delegate void StageValueUpdatedEventHandler(int buildValue, StageData stageData);

    [Signal]
    public delegate void GameStateEnteredEventHandler(GameState gameState);

    [Signal]
    public delegate void GameModeButtonPressedEventHandler();
    
    [Signal]
    public delegate void PlayerHealthUpdatedEventHandler(int health, bool isDead);
    
}