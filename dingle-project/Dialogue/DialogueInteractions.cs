using Godot;
using System;

[GlobalClass]
public partial class DialogueInteractions : Node
{
	[Signal]
	public delegate void WalkToEventHandler(Vector3 targetPosition);

	// Items that can be given
	InventoryItem keyItem;

	public override void _Ready()
	{
		// Load items
		keyItem = GD.Load<InventoryItem>("res://Inventory/Badge.tres");
	}

	public void GiveKey()
	{
		Inventory.AddItem(keyItem);
	}

	public void intro_scene_1() // An Phiast heard the owner and walks over to the chip shop
	{
		GD.Print("An Phiast heard the owner and walks over to the chip shop");
		Vector3 chipShopPosition = new Vector3(20, 0, 0);
		EmitSignal(SignalName.WalkTo, chipShopPosition);
	}
}
