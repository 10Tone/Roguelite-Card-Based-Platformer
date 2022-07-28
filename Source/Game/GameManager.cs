using AutoLoads;
using Godot;

namespace Game;

public class GameManager : Node2D
{
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
    }

    public override void _Ready()
    {
        _globalEvents.EmitSignal(nameof(GlobalEvents.GameReady));
    }
}