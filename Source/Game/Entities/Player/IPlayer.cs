using Tools;

namespace Game;

public interface IPlayer
{
    PlayerStateMachine PlayerStateMachine { get; set; }
    PlayerData PlayerData { get; set; }
    PlayerInputHandler InputHandler { get; set; }
    
}