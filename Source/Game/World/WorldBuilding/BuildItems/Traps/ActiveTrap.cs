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
        _isAnPassiveTrap = false; 
    }

    public void ActivateTrap()
    {
        _iDamageActive = true;
        TrapStateChanged();
    }

    public void IdleTrap()
    {
        _iDamageActive = false;
        TrapStateChanged();
    }
    
}