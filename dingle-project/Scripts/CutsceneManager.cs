using Godot;
using System;
using System.Collections.Generic;

public partial class CutsceneManager : Node
{
	Dictionary<string, Node3D> gameObjects = new Dictionary<string, Node3D>();
	Dictionary<string, Node3D> positions = new Dictionary<string, Node3D>();

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		// Make sure the scene is loaded first
		if (GetTree().CurrentScene == null)
			await ToSignal(GetTree(), SceneTree.SignalName.TreeChanged);


		var allPositions = GetTree().GetNodesInGroup("CutscenePositions");
		var allObjects = GetTree().GetNodesInGroup("CutsceneObjects");

		// Add all positions
		foreach (Node3D pos in allPositions)
		{
			GD.Print(pos.Name, " was added - ", pos);
			positions[pos.Name] = pos;
		}
		// Add all objects
		foreach (Node3D obj in allObjects)
		{
			GD.Print(obj.Name, " was added - ", obj);
			gameObjects[obj.Name] = obj;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void MoveToTarget(string objectName, string targetPosName, float timeTaken = 1.0f)
	{
		if (!gameObjects.ContainsKey(objectName))
		{
			GD.PrintErr($"No object named {objectName} in gameObjects dictionary!");
			return;
		}
		if (!positions.ContainsKey(targetPosName))
		{
			GD.PrintErr($"No position named {targetPosName} in positions dictionary!");
			return;
		}

		if (positions[targetPosName] == null)
		{
			GD.PrintErr("ERROR NULL POS");
		}

		Node3D targetObject = gameObjects[objectName];
		Vector3 targetPos = positions[targetPosName].GlobalPosition;

		// Create a tween
		var tween = GetTree().CreateTween();

		// Animate the GlobalPosition property
		tween.TweenProperty(targetObject, "global_position", targetPos, timeTaken);

		// Optionally wait for it to finish
		GD.Print($"{objectName} finished moving.");
	}
}
