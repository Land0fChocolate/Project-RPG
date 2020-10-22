using Godot;
using System;

public class AbilityCatalog : Node2D
{
	public static (string, string, bool) SwordSwipe(){
		return ("res://Abilities/_001SwordSwipe.tscn", Global.ANI_STATE_ATTACK, false);
	}
	
	public static (string, string, bool) Fireball(){
		return ("res://Abilities/_002Fireball.tscn", Global.ANI_STATE_ATTACK, false);
	}
	
	public static (string, string, bool) Thing(){
		return ("res://Abilities/_003Thing.tscn", Global.ANI_STATE_ATTACK, false);
	}
	
	public static (string, string, bool) StandHeal(){
		return ("res://Abilities/_006StandHeal.tscn", Global.ANI_STATE_PASSIVE, true);
	}
}
