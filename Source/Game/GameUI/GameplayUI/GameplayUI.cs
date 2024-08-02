using System;
using AutoLoads;
using Godot;

namespace Game.GameplayUI;

public partial class GameplayUI : GameUIBase
{
    // protected override void OnGameStateEntered(GameStates gameState)
    // {
    //     base.OnGameStateEntered(gameState);
    //     switch (gameState)
    //     {
    //         case GameStates.PlayMode:
    //             Visible = true;
    //             break;
    //         case GameStates.BuildMode:
    //             Visible = false;
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
    //     }
    // }
}