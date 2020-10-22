using Godot;
using System;

public class Ability : KinematicBody2D
{
	[Export] public string AbilityName;
	[Export] public string SceneAddress;
	[Export] public bool Passive;
	[Export] public int MaxLevel;
	[Export] public float LevelGrowth;
	[Export] public int AttackPower;
	[Export] public int AttackPowerGrowth;
	[Export] public int ShieldPower;
	[Export] public int ShieldPowerGrowth;
	[Export] public int KnockbackPower;
	[Export] public int KnockbackPowerGrowth;
	[Export] public float CastTime;
	[Export] public float Cooldown;
	[Export] public float CooldownGrowth;
	[Export] public int Ammunition;
	[Export] public float CrowdControlLength;
	[Export] public float CrowdControlLengthGrowth;
	[Export] public string ElementalType;
	[Export] public int MonetaryValue;
	[Export] public int MonetaryValueGrowth;
	[Export] public string AttackState;
	[Export] public string UserAnimation;
	[Export] public string Description;
	
	public Vector2 KnockbackVector;
	public int Level;
}
