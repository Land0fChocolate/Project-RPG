using Godot;
using System;

public class NPC : Entity
{
	[Export] public string NpcName;
	[Export] public int WanderRange;
	[Export] public int Quality;
	[Export] public string Description;
	//TODO: add elemental strengths and weaknesses, drops, exp, abilities
	
	public Global.BehaviorState state;
	public PlayerDetectionZone playerDetectionZone = null;
	public Player player = null;
	
	//TODO: could have general methods in here like Idle() and Wander() that have nothing in them, but the children of NPC override them with their own methods.
}
