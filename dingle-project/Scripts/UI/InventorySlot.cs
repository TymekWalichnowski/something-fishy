using Godot;
using System;

public partial class InventorySlot : Panel
{
	public static event Action<InventoryItem> EvidenceChosen;
	
	Sprite2D itemDisplay;
	InventoryItem item; 

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		itemDisplay = GetNode<Sprite2D>("Item Display");
	}

	public void UpdatePanel(InventoryItem t_item)
	{
		item = t_item;
		itemDisplay.Texture = item.Texture;
	}

	public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
		{
			if (item != null)
            {
				GD.Print("InventoryPanel clicked with: ", item.Name);
				EvidenceChosen?.Invoke(item);
            }
        }
    }
}
