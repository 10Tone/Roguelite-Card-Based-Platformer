using AutoLoads;
using Game.LevelSystem;
using Game.WorldBuilding;
using Godot;
using Godot.Collections;
using Tools;

namespace Game;

public partial class LevelValueCalculator : Node
{
   [Export] private bool _useBonusValues = false;
    
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    public int CurrentStageValue { get; private set; }
    
    private Dictionary<Vector2, int> _bonusValues = new Dictionary<Vector2, int>();
    private StageData _currentStage;

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        //
        // _globalEvents.ItemBuild += OnItemBuild;
        // _globalEvents.ItemRemoved += OnItemRemoved;
    }

    public void OnItemBuild(StageData stageData)
    {
        _currentStage = stageData;
        RecalculateBuildItemValues();
    }
    
    public void OnItemRemoved(StageData stageData)
    {
        _currentStage = stageData;
        RecalculateBuildItemValues();
    }
    
    private void UpdateStageValue(int valueChange)
    {
        CurrentStageValue += valueChange;
        DebugOverlay.Instance.DebugPrint(CurrentStageValue.ToString());
        _globalEvents.EmitSignal(nameof(GlobalEvents.StageValueUpdated), CurrentStageValue, _currentStage);
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
            if (_useBonusValues)
            {
                int bonus = CalculateBonus(position, buildedItems);
                _bonusValues[position] = bonus;
                int newValue = baseValue + bonus;
                newTotalValue += newValue;
            }
            else
            {
                newTotalValue += baseValue;
            }
            
        }

        UpdateStageValue(newTotalValue - CurrentStageValue);
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