using Godot;
using System;
using AutoLoads;
using Game.WorldBuilding;

public partial class GridShader : ColorRect
{
    [Export] private Color _gridColor = new Color(1.0f, 1.0f, 1.0f);
    [Export] private float _fadeRadius = 64.0f;
    [Export] private float _fadeStrength = 1.0f;
    
    private GlobalEvents _globalEvents;
    private GlobalVariables _globalVariables;
    private ShaderMaterial _gridShaderMaterial;
    private float _cellSize;

    public override void _EnterTree()
    {
        _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");

        _cellSize = _globalVariables.WorldGridCellSize;
        
        _globalEvents.BuildItemButtonClicked += OnBuildItemButtonClicked;
        _globalEvents.GameStateEntered += ResetTextureToNull;
        
        Visible = false;
        
        Shader gridShader = GD.Load<Shader>("res://Source/Game/GameUI/BuildUI/grid_shader.gdshader");
        _gridShaderMaterial = new ShaderMaterial();
        _gridShaderMaterial.Shader = gridShader;
        Material = _gridShaderMaterial;

        // Set default values for the shader uniforms
        _gridShaderMaterial.SetShaderParameter("grid_color", _gridColor);
        _gridShaderMaterial.SetShaderParameter("cell_size", _cellSize);
        _gridShaderMaterial.SetShaderParameter("fade_radius", _fadeRadius);
        _gridShaderMaterial.SetShaderParameter("fade_strength", _fadeStrength);
    }

    public override void _Process(double delta)
    {
        Vector2 mousePosition = GetGlobalMousePosition();
        float snapSize = _cellSize / 4.0f;
        Vector2 snappedPosition = new Vector2(
            Mathf.Round(mousePosition.X / snapSize) * snapSize,
            Mathf.Round(mousePosition.Y / snapSize) * snapSize
        );
        Vector2 rectSize = Size;
        Position = snappedPosition - rectSize / 2;
        
        Vector2 viewportSize = GetViewportRect().Size;
        _gridShaderMaterial.SetShaderParameter("viewport_size", viewportSize);
        _gridShaderMaterial.SetShaderParameter("mouse_position", snappedPosition);
    }

    private void OnBuildItemButtonClicked(BuildItemResource buildItemResource)
    {
        Visible = true;
    }

    private void ResetTextureToNull(Tools.State gameState)
    {
        Visible = false;
    }
}