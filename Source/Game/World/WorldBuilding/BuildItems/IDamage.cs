using System;
using Godot;

namespace Game.WorldBuilding;

public interface IDamage
{
    event EventHandler PlayerEnteredIDamage;
    void AddToIDamageGroup();
    int GetIDamageValue();
}