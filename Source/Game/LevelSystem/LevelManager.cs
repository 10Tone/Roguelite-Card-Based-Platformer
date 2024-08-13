using Godot;
using System;

namespace LevelSystem;
public partial class LevelManager : Node
{
	[Export] private PackedScene[] _levelScenes;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void LoadNextLevel()
	{
		
	}

	private void OnLevelFinished()
	{
		
	}
}
