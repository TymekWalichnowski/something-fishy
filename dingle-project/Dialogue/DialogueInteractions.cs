using Godot;
using System;

[GlobalClass]
public partial class DialogueInteractions : Node
{
	[Signal]
	public delegate void WalkToEventHandler(Vector3 targetPosition);

	public static InventoryUI inventoryUI;

	// Items that can be given
	InventoryItem badgeItem;

	public override void _Ready()
	{
		// Load items
		badgeItem = GD.Load<InventoryItem>("res://Inventory/Badge.tres");
	}

	public void GiveBadge()
	{
		Inventory.AddItem(badgeItem);
	}

	public void intro_scene_1() // An Phiast heard the owner and walks over to the chip shop
	{
		GD.Print("An Phiast heard the owner and walks over to the chip shop");
		Vector3 chipShopPosition = new Vector3(20, 0, 0);
		EmitSignal(SignalName.WalkTo, chipShopPosition);
	}

	public static void ShowEvidence()
	{
		if (inventoryUI != null && GodotObject.IsInstanceValid(inventoryUI))
		{
			GD.Print("Open");
			inventoryUI.OpenForEvidence();
		}
		else
		{
			GD.Print("Couldn't find inventory UI");
		}
    }
}
