using Godot;
using System;

public class NPC : KinematicBody2D
{
	[Export] public string NpcName;
	[Export] public int Health;
	[Export] public int Acceleration;
	[Export] public int MaxSpeed;
	[Export] public int Friction;
	[Export] public Vector2 Velocity;
	[Export] public Vector2 Direction;
	[Export] public Vector2 NewTargetPosition;
	[Export] public Vector2 Knockback;
	[Export] public int WanderRange;
	[Export] public int Quality;
	[Export] public string Description;
	//TODO: add elemental strengths and weaknesses, drops, exp, abilities
	
	public Global.BehaviorState state;
	public PlayerDetectionZone playerDetectionZone = null;
	public Player player = null;
	
	//TODO: could have general methods in here like Idle() and Wander() that have nothing in them, but the children of NPC override them with their own methods.
}
