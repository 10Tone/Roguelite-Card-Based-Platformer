using Godot;

namespace Game;

public interface IGameUiButton
{
	ButtonData ButtonData { get; }
}
public partial class GameUiButton : Button, IGameUiButton
{
	[Export] private ButtonData _buttonData;
	public ButtonData ButtonData => _buttonData;
}