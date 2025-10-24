using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryUI : Control
{
	private List<InventorySlot> slots = new List<InventorySlot>();
	bool isOpen = false;

	// Button
	Button noEvidenceButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Give this to DialogueInteractions
		DialogueInteractions.inventoryUI = this;

		noEvidenceButton = GetNode<Button>("NoEvidence");
		noEvidenceButton.Pressed += DialogueInteractions.CancelEvidence;

		// Get the GridContainer
		GridContainer grid = GetNode<GridContainer>("NinePatchRect/GridContainer");

		// Loop through all children of the GridContainer
		foreach (Node child in grid.GetChildren())
		{
			if (child is InventorySlot slot)
			{
				slots.Add(slot);
			}
		}
		
		Close();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("OpenInventory") && Player.CanInteract())
		{
			if (isOpen)
			{
				Close();
			}
			else
			{  
				Open();
			}
		}
	}

	public void Open(bool showNoEvidenceButton = false)
	{
		Visible = true;
		isOpen = true;

		if (showNoEvidenceButton)
		{
			noEvidenceButton.Visible = true;
		}
		else
		{
            noEvidenceButton.Visible = false;
        }

		for (int i = 0; i < Inventory.MAX_SIZE; i++)
		{
			if (i < Inventory.GetSize())
			{
				slots[i].UpdatePanel(Inventory.GetItemAtIndex(i));
			}
		}
	}
	
	public void OpenForEvidence()
	{
		Open();
		GD.Print("Await for option selected");
		// Selections logic
		GD.Print("On selection return selected Item");
	}
	
	public void Close()
	{
		Visible = false;
		isOpen = false;
	}
	
}
