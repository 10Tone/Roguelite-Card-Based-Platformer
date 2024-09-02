using System;
using AutoLoads;
using Godot;
using Tools;

namespace Game.GameplayUI;

public partial class GameplayUI : CanvasLayer
{
    [Export] private PackedScene _buildButtonScene, _playButtonScene;

    [Export()] private NodePath _buildPlayButtonsPath;

    // private GameStates _gameState;
    private GameUiButton _gameModeButton;
    private Control _buildPlayButtonsContainer;

    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;


    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _globalEvents.GameStateEntered += OnGameStateEntered;
    }

    public override void _Ready()
    {
        _buildPlayButtonsContainer = GetNode(_buildPlayButtonsPath) as Control;

    }


    private void OnGameStateEntered(GameState gameState)
    {
        PackedScene buttonScene = null;

        if (gameState == _globalVariables.GameStates[GameModeState.PlayModeState])
        {
            buttonScene = _buildButtonScene;
        }
        else if (gameState == _globalVariables.GameStates[GameModeState.BuildModeState])
        {
            buttonScene = _playButtonScene;
        }

        if (buttonScene == null) return;
        _gameModeButton?.QueueFree();
        _gameModeButton = buttonScene.Instantiate() as GameUiButton;
        _buildPlayButtonsContainer.AddChild(_gameModeButton);

        if (_gameModeButton != null)
        {
            _gameModeButton.Pressed += OnGameModeButtonPressed;
        }
    }


    private void OnGameModeButtonPressed()
    {
        switch (_gameModeButton.ButtonData.ButtonType)
        {
            case ButtonType.Build:
                DebugOverlay.Instance.DebugPrint(_gameModeButton.ButtonData.ButtonType + " button pressed");
                _globalEvents.EmitSignal(nameof(GlobalEvents.GameUiButtonPressed),
                    (int)_gameModeButton.ButtonData.ButtonType);


                break;
            case ButtonType.Play:
                DebugOverlay.Instance.DebugPrint(_gameModeButton.ButtonData.ButtonType + " button pressed");
                _globalEvents.EmitSignal(nameof(GlobalEvents.GameUiButtonPressed),
                    (int)_gameModeButton.ButtonData.ButtonType);


                break;
        }
    }
}