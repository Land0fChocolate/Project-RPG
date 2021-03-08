using Godot;
using System;

public class Inventory : Node
{
	/*
	//signal ItemsChanged(Indexes)
	
	[Export] public Resource[] Items = [];
	
	public Resource SetItem(int ItemIndex, Resource Item){
		var previousItem = Items[ItemIndex];
		Items[ItemIndex] = Item;
		EmitSignal("ItemsChanged", ItemIndex);
		return previousItem;
	}
	
	public void SwapItem(int ItemIndex, int TargetItemIndex){
		var targetItem = Items[TargetItemIndex];
		var item = Items[ItemIndex];
		Items[TargetItemIndex] = item;
		Items[ItemIndex] = targetItem;
		EmitSignal("ItemsChanged", ItemIndex, TargetItemIndex);
		return previousItem;
	}
	
	public Resource RemoveItem(int ItemIndex){
		var previousItem = Items[ItemIndex];
		Items[ItemIndex] = null;
		EmitSignal("ItemsChanged", ItemIndex);
		return previousItem;
	}
	*/
}
