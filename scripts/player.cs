using Godot;
using System;

public partial class player : CharacterBody2D
{	[Export]
	public float Speed = 300.0f;
	[Export]
	public float JumpVelocity = -500.0f;
	bool jumping = false;
	

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSpriteX");
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor()){
			velocity.Y += gravity * (float)delta;
		}
		else
		{
			jumping = false;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor()){
			velocity.Y = JumpVelocity;
			jumping = true;
			animatedSprite2D.Play("jump");
			//GD.Print("hello from VS code!");
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			animatedSprite2D.FlipH = velocity.X < 0;
			if (!jumping)
			{	
				animatedSprite2D.Play("run");
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if (jumping)
			{	
				animatedSprite2D.Play("jump");
			}
			else
			{
				animatedSprite2D.Play("idle");
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
