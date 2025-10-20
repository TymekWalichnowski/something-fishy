using Godot;
using System;

public partial class CutsceneTrigger : Area3D
{
	Cutscene cutscene;

	bool playerInside = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		cutscene = GetParent<Cutscene>();
		BodyEntered += OnBodyEntered;
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
			if (cutscene.HasMethod("StartCutscene") || cutscene != null)
			{
				cutscene.Call("StartCutscene");
				var collisionShape = GetNode<CollisionShape3D>("CollisionShape3D");
				collisionShape.SetDeferred("disabled", true);
			}
			else
			{
				GD.Print("ERROR - Cutscene not found in " + cutscene.Name);
			}
		}
	}
}
