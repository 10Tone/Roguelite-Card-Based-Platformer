using System;
using System.Collections.Generic;
using Tools;

namespace Game;

public partial class PlayerStateMachine: StateMachine
{
    public Godot.Collections.Dictionary<PlayerStates.PlayerStates, State> States = new();
}

