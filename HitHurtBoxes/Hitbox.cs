using Godot;
using System;

public class Hitbox : Area2D
{
	public int Damage;
	public Vector2 KnockbackVector = Vector2.Zero;
}
