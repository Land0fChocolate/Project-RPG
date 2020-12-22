using Godot;
using System;

public class Entity : KinematicBody2D
{
	[Export] public int Health;
	[Export] public int Acceleration;
	[Export] public int MaxSpeed;
	[Export] public int Friction;
	[Export] public Vector2 Velocity;
	[Export] public Vector2 Direction;
	[Export] public Vector2 NewTargetPosition;
	[Export] public Vector2 Knockback;
}

//TODO: Player, NPC, Grass, etc will all descend from Entity.
