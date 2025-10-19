using DialogueManagerRuntime;
using Godot;
using System;

public partial class Chipper_Intro : Node3D
{
	[Export] public Resource dialogue;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DialogueManager.DialogueEnded += OnDialogueEnded;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Interact()
	{
		DialogueManager.ShowDialogueBalloon(dialogue);
	}

	private void OnDialogueEnded(Resource resource)
	{
		GD.Print("Dialogue ended! Resource: " + resource);
		Player.ToggleMove(true);
		Player.ToggleInteract(true);
	}
}
