using Godot;
using System;
using System.Collections.Generic;
using DialogueManagerRuntime;

public partial class Cutscene : Node
{
	[Export] public Resource dialogue;
	
	[Export] public Godot.Collections.Dictionary<string, Node3D> gameObjects;
	[Export] public Godot.Collections.Dictionary<string, Node3D> positions;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		DialogueManager.DialogueEnded += OnDialogueEnded;
		
		// Make a dictionary of available references
		var state = new Godot.Collections.Dictionary<string, Variant>
		{
			{ "ChipperCutscene", this }
		};

		// Give the dialogue manager both the dialogue resource and the state dictionary
		DialogueManager.ShowDialogueBalloon(dialogue, "start", new Godot.Collections.Array<Variant> { state });
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void StartCutscene()
	{
		// Freeze Player
		Player.ToggleMove(false);
		Player.ToggleInteract(false);

		// Name the object so dialogue can reference it
		var state = new Godot.Collections.Dictionary<string, Variant>
		{
			{ "Cutscene", this }
		};

		// Give the dialogue this as a variable
		var extra = new Godot.Collections.Array<Variant> { this };
    	DialogueManager.ShowDialogueBalloon(dialogue, "start", extra);
    }

	public void MoveToTarget(string objectName, string targetPosName)
	{
		if (!gameObjects.ContainsKey(objectName))
		{
			GD.PrintErr($"No object named {objectName} in gameObjects dictionary!");
			return;
		}

		Node3D targetObject = gameObjects[objectName];
		Vector3 targetPos = positions[targetPosName].GlobalPosition;

		// Create a tween
		var tween = GetTree().CreateTween();

		// Animate the GlobalPosition property
		tween.TweenProperty(targetObject, "global_position", targetPos, 5);

		// Optionally wait for it to finish
		GD.Print($"{objectName} finished moving.");
	}
	

	
	private void OnDialogueEnded(Resource resource)
	{
		GD.Print("Dialogue ended! Resource: " + resource);
		Player.ToggleMove(true);
		Player.ToggleInteract(true);
	}
}
