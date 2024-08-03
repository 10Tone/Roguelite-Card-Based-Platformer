using System;
using AutoLoads;
using Godot;
using Tools;

namespace Game;

public partial class GameUIBase : CanvasLayer
{
    [Export()] protected GameStates GameState;
    
    protected GlobalEvents _globalEvents;
    protected GlobalVariables _globalVariables;
    
    

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        // _globalEvents.Connect(nameof(GlobalEvents.GameStateEnteredEventHandler), new Callable(this, nameof(OnGameStateEntered)));
        _globalEvents.GameStateEntered += OnGameStateEntered;
    }
    

    protected virtual void OnGameStateEntered(GameStates gameState)
    {

    }


}