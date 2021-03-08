using Godot;
using System;

public class InventoryDisplay : GridContainer
{
/*
	public override void _Ready(){
		//preload inventory
		Inventory inventory = (Inventory)ResourceLoader.Load("res://UI/Inventory.tscn");
			
		stats = (Stats)GetNode("Stats");
		inventory.Connect("ItemsChanged", this, "OnItemsChanged");
		
		UpdateInventoryDisplay();
	}
	
	public void UpdateInventoryDisplay(){
		foreach (int itemIndex in inventory.Items.Size()){
			UpdateInventorySlotDisplay(itemIndex);
		}
	}
	
	public void UpdateInventorySlotDisplay(int itemIndex){
		var inventorySlotDisplay = GetChild(itemIndex);
		var item = inventory.Items[itemIndex];
		inventorySlotDisplay.DisplayItem(item);
	}
	
	public void OnItemsChanged([]int indexes){
		foreach (int itemIndex in indexes) {
			UpdateInventorySlotDisplay(itemIndex)
		}
	}
	*/
}
