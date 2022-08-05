using System;
using Godot;

namespace Game.WorldBuilding;

public class BuildItemResource : Resource
{
  [Export()] public String Name { get; set; }
  [Export()] public Texture Icon { get; set; }
  [Export()] public PackedScene Scene { get; set; }
  
}