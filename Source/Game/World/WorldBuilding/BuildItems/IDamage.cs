using System;
using Godot;

namespace Game.WorldBuilding;

public interface IDamage
{
    event EventHandler IDamageActive;
    void AddToIDamageGroup();
    int GetIDamageValue();
    bool GetTrapActive();
    bool PlayerEnteredIDamage { get; set; }
}