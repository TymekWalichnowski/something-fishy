using Godot;
using System;

public partial class TriggerZone : Area3D
{
	bool playerInside = false;
	Node parent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;

		parent = GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnBodyEntered(Node3D body)
	{
		if (body is Player)
		{
			GD.Print("Enter");
			playerInside = true;
		}
		
		if (playerInside)
		{
			if (parent.HasMethod("Interact") || parent != null)
			{
				parent.Call("Interact");
				Player.ToggleMove(false);
				Player.ToggleInteract(false);
				var collisionShape = GetNode<CollisionShape3D>("CollisionShape3D");
				collisionShape.SetDeferred("disabled", true);
			}
			else
			{
				GD.Print("ERROR - Interact not found in " + parent.Name);
			}
		}
	}
	private void OnBodyExited(Node3D body)
	{
		if (body is Player)
		{
			GD.Print("Exit");
			playerInside = false;
		}
	}
}
