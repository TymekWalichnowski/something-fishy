using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	private static bool canMove = true;
	private static bool canInteract = true;
	private Vector3? walkTarget = null;

	// Camera Data
	Node3D camera;
	Transform3D originalCameraTransform;

	static Sprite3D interactIcon;

    public override void _Ready()
    {
		camera = GetNode<Node3D>("Camera");
		originalCameraTransform = camera.Transform;

		interactIcon = GetNode<Sprite3D>("InteractIcon");
		HideInteract();
    }


	public override void _PhysicsProcess(double delta)
	{
		if (canMove) // normal movement
		{
			Movement(delta);
		}
		else if (walkTarget != null) // cutscene walking code if has target
		{
			WalkTowardsTarget((float)delta);
		}

		// Dont display icon while interacting
		if (!canMove && interactIcon.Visible)
		{
			HideInteract();
		}
	}

	// Can be used to freeze the player whenever wanted
	public static void ToggleMove(bool t_canMove = true)
	{
		canMove = t_canMove;
	}
	public static bool CanMove() { return canMove; }
	
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

	public static void ShowInteract()
	{
		interactIcon.Visible = true;
	}
	public static void HideInteract()
    {
        interactIcon.Visible = false;
    }
	
	public void WalkTo(Vector3 target) // Setting target and disabling movement
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

	public void ResetCameraTween(float t_timeTaken)
    {
        var tween = GetTree().CreateTween();
        // Tween back to the cached transform (position + rotation + scale)
        tween.TweenProperty(camera, "transform", originalCameraTransform, t_timeTaken);
    }
}
