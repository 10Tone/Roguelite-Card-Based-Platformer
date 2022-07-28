using Godot;

namespace AutoLoads;

public class GlobalEvents : Node
{
    [Signal]
    public delegate void GameReady();
}