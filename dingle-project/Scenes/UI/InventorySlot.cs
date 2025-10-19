using Godot;
using System;

public partial class InventorySlot : Panel
{
	Sprite2D itemDisplay;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		itemDisplay = GetNode<Sprite2D>("Item Display");
	}

	public void UpdatePanel(InventoryItem t_item)
	{
		itemDisplay.Texture = t_item.Texture;
	}
}
