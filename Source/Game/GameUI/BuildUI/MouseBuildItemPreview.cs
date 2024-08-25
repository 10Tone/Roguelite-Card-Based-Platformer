using Godot;
using System;
using AutoLoads;
using Game;
using Game.WorldBuilding;

public partial class MouseBuildItemPreview : Sprite2D
{
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    private float _cellSize;

    private float _snapSize = 4;
    // private Vector2 _offSet = new Vector2(-16, -16);

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
        _cellSize = _globalVariables.WorldGridCellSize;
        // _globalEvents.Connect(nameof(GlobalEvents.BuildItemButtonClickedEventHandler), new Callable(this, nameof(OnBuildItemButtonClicked)));
        // _globalEvents.Connect(nameof(GlobalEvents.GameStateEnteredEventHandler), new Callable(this, nameof(ResetTextureToNull)));

        _globalEvents.BuildItemButtonClicked += OnBuildItemButtonClicked;
        _globalEvents.GameStateEntered += ResetTextureToNull;

        Texture = null;
    }

    public override void _Process(double delta)
    {
        Vector2 mousePosition = GetGlobalMousePosition();
        mousePosition.X = Mathf.Floor(mousePosition.X / (_cellSize / _snapSize)) * (_cellSize / _snapSize);
        mousePosition.Y = Mathf.Floor(mousePosition.Y / (_cellSize / _snapSize)) * (_cellSize / _snapSize);
        Position = mousePosition;
    }


    private void OnBuildItemButtonClicked(BuildItemResource buildItemResource)
    {
        Texture = buildItemResource.Icon;
    }

    private void ResetTextureToNull(Tools.State gameState)
    {
        Texture = null;
    }
}