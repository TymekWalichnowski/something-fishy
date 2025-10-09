using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	private static bool canMove = true;
	private static bool canInteract = true;

	public override void _PhysicsProcess(double delta)
	{
		if (canMove)
		{
			Movement(delta);
		}
	}

	// Can be used to freeze the player whenever wanted
	public static void ToggleMove(bool t_canMove = true)
	{
		canMove = t_canMove;
	}
	public static bool CanMove() {return canMove;}
	// Stop player interactions with interact-key
	public static void ToggleInteract(bool t_canInteract = true)
	{
		canInteract = t_canInteract;
	}
	public static bool CanInteract() {return canInteract;}


	void Movement(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
