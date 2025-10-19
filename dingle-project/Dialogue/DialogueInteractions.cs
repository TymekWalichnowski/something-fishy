using Godot;
using System;

[GlobalClass]
public partial class DialogueInteractions : Node
{
	[Signal]
	public delegate void WalkToEventHandler(Vector3 targetPosition);

	public void GiveKey()
	{
		GD.Print("KEY GIVEN!");
	}

	public void intro_scene_1() // An Phiast heard the owner and walks over to the chip shop
	{
		GD.Print("An Phiast heard the owner and walks over to the chip shop");
		Vector3 chipShopPosition = new Vector3(20, 0, 0);
		EmitSignal(SignalName.WalkTo, chipShopPosition);
	}
}
