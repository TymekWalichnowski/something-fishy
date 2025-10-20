using Godot;
using System;
using System.Collections.Generic;
using DialogueManagerRuntime;

public partial class Cutscene : Node
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

	public void StartCutscene()
	{
		// Freeze Player
		Player.ToggleMove(false);
		Player.ToggleInteract(false);

    	DialogueManager.ShowDialogueBalloon(dialogue);
    }
	

	
	private void OnDialogueEnded(Resource resource)
	{
		GD.Print("Dialogue ended! Resource: " + resource);
		Player.ToggleMove(true);
		Player.ToggleInteract(true);
	}
}
