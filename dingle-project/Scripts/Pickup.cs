using Godot;
using System;
using DialogueManagerRuntime;

public partial class Pickup : Node
{
	[Export] public Resource dialogue;

	public void Interact()
	{
		DialogueManager.ShowDialogueBalloon(dialogue);
	}
}
