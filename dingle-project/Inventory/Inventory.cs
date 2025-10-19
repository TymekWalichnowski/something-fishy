using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
	static Array<InventoryItem> inventory = new Array<InventoryItem>();
	public const int MAX_SIZE = 16;

	public static InventoryItem GetItemAtIndex(int i)
	{
		return inventory[i];
	}
	
	public static int GetSize()
    {
		return inventory.Count;
    }

	public static bool HasItem(string t_name)
	{
		foreach (InventoryItem item in inventory)
		{
			if (item.Name == t_name)
			{
				return true;
			}
		}

		return false;
	}

	public static void AddItem(InventoryItem t_newItem)
	{
		if (inventory.Count < MAX_SIZE)
        {
			inventory.Add(t_newItem);
        }
	}
}
