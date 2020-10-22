using Godot;
using System;

public class Stats : Node
{
	[Export] public int MaxHealth = 4;
	[Signal] public delegate void NoHealth();
	[Signal] public delegate void HealthChanged(int value);
	public int health;
	public int Health{
		get{
			return this.health;
		}
		set{
			if (value <= MaxHealth){
				this.health = value;
			}
			EmitSignal("HealthChanged", health);
			if (health <= 0){
				EmitSignal("NoHealth");
			}
		}
	}

	public override void _Ready(){
		health = MaxHealth;
	}
}
