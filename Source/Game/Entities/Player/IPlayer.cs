using Game.WorldBuilding;
using Godot;
using Tools;

namespace Game;

public interface IPlayer
{
    PlayerStateMachine PlayerStateMachine { get; set; }
    PlayerData PlayerData { get; set; }
    PlayerInputHandler InputHandler { get; set; }
    Vector2 PlayerVelocity { get; set; }
    bool IsGrounded { get; set; }
    AnimatedSprite2D AnimatedSprite { get; set; }
    
    void CheckIfShouldFlip(float horizontalInput);
    
}
    
    