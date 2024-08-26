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
  [Export] public int BuildItemValue { get; set; }
  [Export] public int DamageValue { get; set; }
}