using Godot;
using System;

public class Pin : Node
{
	//NOTE: may use Ability instead of AbilityAddress and UserAnimation so that when stats are loaded in menus, it just loads from the pin instead of 
	//having to load a scene every time.
	public string AbilityAddress;
	public string UserAnimation; //this shouldn't be here. Pin should have an Ability field for Ability details.
	public bool Passive; //this shouldn't be here. Pin should have an Ability field for Ability details.
	public int Level;
	public int EXP;
	public int Quality;
	public int SlotNum; //if = 0, then it is unassigned
}
