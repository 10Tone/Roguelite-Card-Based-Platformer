using AutoLoads;
using Godot;
using Tools;

namespace Game;

public partial class LevelValueCalculator: Node
{
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;

    private int _levelValue = 0;

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");

        // _globalEvents.Connect(nameof(GlobalEvents.ItemBuildEventHandler), new Callable(this, nameof(OnItemBuild)));
        // _globalEvents.Connect(nameof(GlobalEvents.ItemRemovedEventHandler), new Callable(this, nameof(OnItemRemoved)));
        
        _globalEvents.ItemBuild += OnItemBuild;
        _globalEvents.ItemRemoved += OnItemRemoved;
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
        DebugOverlay.Instance.DebugPrint(_levelValue.ToString());
        _globalEvents.EmitSignal(nameof(GlobalEvents.LevelValueUpdated), _levelValue);
    }
}