using Godot;
using System;
using AutoLoads;
using Game;

public partial class GameplayFeedback : Control
{
	private GlobalEvents _globalEvents;
	private GlobalVariables _globalVariables;

	public override void _EnterTree()
	{
		_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
		_globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
		
		_globalEvents.GameStateEntered += OnGameStateEntered;
	}

	private void OnGameStateEntered(GameState gamestate)
{
    switch (gamestate)
    {
        case var _ when gamestate == _globalVariables.GameStates[GameModeState.PlayModeState]:

            break;
        case var _ when gamestate == _globalVariables.GameStates[GameModeState.BuildModeState]:

            break;
        case var _ when gamestate == _globalVariables.GameStates[GameModeState.DeathModeState]:

            break;
        case var _ when gamestate == _globalVariables.GameStates[GameModeState.LevelFinishedModeState]:

            break;
        case var _ when gamestate == _globalVariables.GameStates[GameModeState.StageFinishedModeState]:

            break;
    }
}

}

