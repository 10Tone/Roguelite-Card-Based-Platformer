using Godot;
using System;
using AutoLoads;
using Game;

public partial class GameplayFeedback : Control
{
    [Export()] private string _playMessage, _buildMessage, _deathMessage, _levelFinishedMessage, _stageFinishedMessage;
    
	private GlobalEvents _globalEvents;
	private GlobalVariables _globalVariables;

    private Label _mainTextLabel;

	public override void _EnterTree()
	{
		_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
		_globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
		
		_globalEvents.GameStateEntered += OnGameStateEntered;
	}

    public override void _Ready()
    {
        _mainTextLabel = GetNode<Label>("MainTextLabel");
        _mainTextLabel.Text = "";
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
            break;
        case var _ when gamestate == _globalVariables.GameStates[GameModeState.DeathModeState]:
            _mainTextLabel.Text = _deathMessage;
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

