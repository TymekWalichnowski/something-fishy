using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	private static bool canMove = true;
	private static bool canInteract = true;
	private Vector3? walkTarget = null;
	

	public override void _PhysicsProcess(double delta)
	{
		if (walkTarget != null) // cutscene walking code if has target
		{
			GD.Print("cutscene movement");
			WalkTowardsTarget((float)delta);
		}
		else if (canMove) // normal movement
		{
			GD.Print("normal movement");
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
	
	public void WalkTo(Vector3 target) //setting target and disabling movement
	{
		walkTarget = target;
		canMove = false;
	}
	private void WalkTowardsTarget(float delta)
	{
	Vector3 target = walkTarget.Value;
	Vector3 toTarget = target - GlobalTransform.Origin;
	float distance = toTarget.Length();

	if (distance > 3.0f) // This won't get you exactly there, but close enough
	{
		Vector3 direction = toTarget.Normalized();
		Vector3 velocity = direction * Speed;

		velocity.Y = Velocity.Y;
		Velocity = velocity;

		MoveAndSlide();
	}
	else
	{
		GD.Print("Arrived at target");
		Velocity = Vector3.Zero;
		walkTarget = null;
	}
}
}
