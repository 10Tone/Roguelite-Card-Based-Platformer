using Godot;
using System;

public partial class PlayerDebug : CharacterBody2D
{
	private const float Gravity = 200.0f;
	private const int WalkSpeed = 200;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;

		velocity.Y += (float)delta * Gravity;

		if (Input.IsActionPressed("ui_left"))
		{
			velocity.X = -WalkSpeed;
		}
		else if (Input.IsActionPressed("ui_right"))
		{
			velocity.X = WalkSpeed;
		}
		else
		{
			velocity.X = 0;
		}

		Velocity = velocity;

		// "MoveAndSlide" already takes delta time into account.
		MoveAndSlide();
	}
}
