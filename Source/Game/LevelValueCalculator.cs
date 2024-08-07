using AutoLoads;
using Game.WorldBuilding;
using Godot;
using Godot.Collections;
using Tools;

namespace Game;

public partial class LevelValueCalculator : Node
{
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    private int _levelValue = 0;
    
    private Dictionary<Vector2, int> _bonusValues = new Dictionary<Vector2, int>();

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        
        _globalEvents.ItemBuild += OnItemBuild;
        _globalEvents.ItemRemoved += OnItemRemoved;
    }

    private void OnItemBuild(BuildItemResource buildItemResource, Dictionary<Vector2, Node2D> neighbors)
    {
        RecalculateBuildItemValues();
    }
    
    private void OnItemRemoved(BuildItemResource buildItemResource, Dictionary<Vector2, Node2D> neighbors)
    {
        RecalculateBuildItemValues();
    }
    
    private void UpdateLevelValue(int valueChange)
    {
        _levelValue += valueChange;
        DebugOverlay.Instance.DebugPrint(_levelValue.ToString());
        _globalEvents.EmitSignal(nameof(GlobalEvents.LevelValueUpdated), _levelValue);
    }

    private void RecalculateBuildItemValues()
    {
        var buildedItems = _globalVariables.BuildedItems;
        int newTotalValue = 0;

        foreach (var (position, node) in buildedItems)
        {
            if (node is not IBuildItem buildItem) continue;

            BuildItemResource resource = buildItem.BuildItemResource;
            if (resource.BuildItemType == BuildItemTypes.Platforms)
            {
                _bonusValues[position] = 0;
                continue;
            }

            int baseValue = resource.BuildItemValue;
            int bonus = CalculateBonus(position, buildedItems);
            _bonusValues[position] = bonus;
            int newValue = baseValue + bonus;
            newTotalValue += newValue;
        }

        UpdateLevelValue(newTotalValue - _levelValue);
    }

    private int CalculateBonus(Vector2 position, Dictionary<Vector2, Node2D> buildedItems)
    {
        int bonus = 0;
        Vector2[] directions = { Vector2.Left, Vector2.Right, Vector2.Up };

        foreach (Vector2 direction in directions)
        {
            Vector2 neighborPos = position + direction;
            if (buildedItems.TryGetValue(neighborPos, out var neighborNode) && neighborNode is IBuildItem neighborItem)
            {
                if (neighborItem.BuildItemResource.BuildItemType != BuildItemTypes.Platforms)
                {
                    bonus += 1;
                }
                else if (direction == Vector2.Up)
                {
                    bonus -= 1;
                }
            }
        }

        return bonus;
    }
}