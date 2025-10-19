using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryUI : Control
{
	private List<InventorySlot> slots = new List<InventorySlot>();
	bool isOpen = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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

	public void Open()
	{
		Visible = true;
		isOpen = true;

		for (int i = 0; i < Inventory.MAX_SIZE; i++)
		{
			if (i < Inventory.GetSize())
            {
             	slots[i].UpdatePanel(Inventory.GetItemAtIndex(i));   
            }
        }
	}
	
	public void Close()
    {
		Visible = false;
		isOpen = false;
    }
}