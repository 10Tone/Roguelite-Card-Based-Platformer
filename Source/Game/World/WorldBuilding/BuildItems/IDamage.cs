using Godot;

namespace Game.WorldBuilding;

public interface IDamage
{
    void OnPlayerEntered(Node2D player);
}