using Godot;
using System;
using AutoLoads;
using Game;

public partial class GameplayFeedback : Control
{
    [Export()] private string _playMessage, _buildMessage, _deathMessage, _levelFinishedMessage, _stageFinishedMessage;
    [Export] private NodePath _mainTextLabelPath, _gameOverButtonsPath;
	private GlobalEvents _globalEvents;
	private GlobalVariables _globalVariables;

    private Label _mainTextLabel;
    private VBoxContainer _gameOverButtons;
    private ButtonData _replayButtonData, _quitButtonData;

	public override void _EnterTree()
	{
		_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
		_globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
		
		_globalEvents.GameStateEntered += OnGameStateEntered;
	}

    public override void _Ready()
{
    _mainTextLabel = GetNode<Label>(_mainTextLabelPath);
    _mainTextLabel.Text = "";
    _gameOverButtons = GetNode<VBoxContainer>(_gameOverButtonsPath);
    _gameOverButtons.Visible = false;

    foreach (var button in _gameOverButtons.GetChildren())
    {
        if (button is not Button b) continue;

        var gameUiButton = b as IGameUiButton;

        switch (gameUiButton?.ButtonData.ButtonType)
        {
            case ButtonType.Replay:
                _replayButtonData = gameUiButton.ButtonData;
                b.Pressed += OnReplayButtonPressed;
                break;
            case ButtonType.Quit:
                _quitButtonData = gameUiButton.ButtonData;
                b.Pressed += OnQuitButtonPressed;
                break;
        }
    }
}

    private void OnQuitButtonPressed()
    {
        _globalEvents.EmitSignal(nameof(_globalEvents.GameUiButtonPressed), (int)_quitButtonData.ButtonType);
    }

    private void OnReplayButtonPressed()
    {
        _globalEvents.EmitSignal(nameof(_globalEvents.GameUiButtonPressed), (int)_replayButtonData.ButtonType);
    }
    

    private void OnGameStateEntered(GameState gamestate)
{
    switch (gamestate)
    {
        case var _ when gamestate == _globalVariables.GameStates[GameModeState.PlayModeState]:
            _mainTextLabel.Text = _playMessage;
            break;
        case var _ when gamestate == _globalVariables.GameStates[GameModeState.BuildModeState]:
            _mainTextLabel.Text = _buildMessage;
            // _gameOverButtons.Visible = false;
            break;
        case var _ when gamestate == _globalVariables.GameStates[GameModeState.DeathModeState]:
            _mainTextLabel.Text = _deathMessage;
            _gameOverButtons.Visible = true;
            break;
        case var _ when gamestate == _globalVariables.GameStates[GameModeState.LevelFinishedModeState]:
            _mainTextLabel.Text = _levelFinishedMessage;
            break;
        case var _ when gamestate == _globalVariables.GameStates[GameModeState.StageFinishedModeState]:
            _mainTextLabel.Text = _stageFinishedMessage;
            break;
    }
}

}

