using AutoLoads;
using Godot;

namespace Game;

public class LevelValueCalculator: Node
{
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;

    private int _levelValue = 0;

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");

        _globalEvents.Connect(nameof(GlobalEvents.ItemBuild), this, nameof(OnItemBuild));
        _globalEvents.Connect(nameof(GlobalEvents.ItemRemoved), this, nameof(OnItemRemoved));
    }

    private void OnItemBuild(int value)
    {
        UpdateLevelValue(value);
    }

    private void OnItemRemoved(int value)
    {
        UpdateLevelValue(-value);
    }

    private void UpdateLevelValue(int value)
    {
        _levelValue += value;
        GD.Print(_levelValue);
        _globalEvents.EmitSignal(nameof(GlobalEvents.LevelValueUpdated), _levelValue);
    }
}