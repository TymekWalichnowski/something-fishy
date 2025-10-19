using DialogueManagerRuntime;
using Godot;
using System;
using System.Threading.Tasks;

[GlobalClass]
public partial class DialogueInteractions : Node
{
	[Signal]
	public delegate void WalkToEventHandler(Vector3 targetPosition);

	public static InventoryUI inventoryUI;

	// Evidence Item Shown
	static InventoryItem currentlyShownItem = null;

	// Items that can be given
	InventoryItem badgeItem;

	public override void _Ready()
	{
		InventorySlot.EvidenceChosen += OnEvidenceChosen;

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
		if (inventoryUI != null)
		{
			GD.Print("Open");
			inventoryUI.Open();
		}
		else
		{
			GD.Print("Couldn't find inventory UI");
		}
	}

	public static void ClearEvidenceItem()
    {
		currentlyShownItem = null;
    }

	public static string GetCurrentItemName()
	{
		if (currentlyShownItem == null)
		{
			return "Nothing";
		}

		return currentlyShownItem.Name;
	}

	private void OnEvidenceChosen(InventoryItem chosen)
    {
        currentlyShownItem = chosen;
        GD.Print($"Evidence chosen: {currentlyShownItem.Name}");

        // You can trigger any follow-up logic here
        // e.g., send a signal, continue dialogue, close UI, etc.
    }
}
