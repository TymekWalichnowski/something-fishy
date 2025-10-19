using Godot;
using System;

[Signal]
public delegate void WalkTo(Vector3 targetPosition);

public partial class DialogueInteractions : Node
{
	public void GiveKey()
	{
		GD.Print("KEY GIVEN!");
	}

	public void intro_scene_1() // An Phiast heard the owner and walks over to the chip shop
	{
		GD.Print("An Phiast heard the owner and walks over to the chip shop");
		Vector3 chipShopPosition = new Vector3(20, 0, 0);
		EmitSignal("WalkTo", chipShopPosition);
	}
}
