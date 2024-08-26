using AutoLoads;
using Godot;

namespace Game;

public partial class PlayerData : Resource
{
    [ExportGroup("Movement")]
    [Export] public float Acceleration = 512;
    [Export] public float AirResistance = 0.02f;
    [Export] public int AmountOfJumps = 2;
    [Export] public float CoyoteTime = 0.2f;
    [Export] public float Friction = 0.25f;
    [Export] public float Gravity = 200;
    [Export] public float JumpForce = 128;
    [Export] public float MaxSpeed = 64;
    [Export] public float VariableJumpHeightMultiplier = 0.1f;
    
    [ExportGroup("Health")]
    [Export] public int Health = 100;
    [Export] public int MaxHealth = 100;
}