using Godot;
using System;
using Game.WorldBuilding;
using Tools;

namespace Game.WorldBuilding;

public partial class ActiveTrap: Trap
{

    public override void _EnterTree()
    {
        base._EnterTree();
    }

    public void ActivateTrap()
    {
        _trapActive = true;
        TrapStateChanged();
    }

    public void IdleTrap()
    {
        _trapActive = false;
        TrapStateChanged();
    }
    
}