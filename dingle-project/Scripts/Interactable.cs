using Godot;
using System;

public partial class Interactable : Area3D
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

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("Interact") && Player.CanInteract())
		{
			if (playerInside)
			{
				if (parent.HasMethod("Interact") || parent != null)
				{
					parent.Call("Interact");
					Player.ToggleMove(false);
					Player.ToggleInteract(false);
				}
				else
				{
					GD.Print("ERROR - Interact not found in " + parent.Name);
				}
			}
		}
	}

	private void OnBodyEntered(Node3D body)
	{
		if (body is Player)
		{
			GD.Print("Enter");
			playerInside = true;
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
