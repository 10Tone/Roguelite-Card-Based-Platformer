using System;
using Godot;

namespace Game.WorldBuilding;
[GlobalClass]
public partial class BuildItemResource : Resource
{
    [Export()] public String Name { get; set; }
    [Export()] public Texture2D Icon { get; set; }
    [Export()] public PackedScene Scene { get; set; }
    [Export()] public BuildItemTypes BuildItemType { get; set; }
    [Export] public int BuildItemValue { get; private set; }
    [Export] public int DamageValue { get; private set; }

    [Export] private bool _initialUnlockState;
    
    private bool _isUnlocked;
    public bool IsUnlocked
    {
        get => _isUnlocked;
        set => _isUnlocked = value;
    }

    public BuildItemResource()
    {
        ResetToInitialState();
    }

    public void ResetToInitialState()
    {
        _isUnlocked = _initialUnlockState;
    }
}