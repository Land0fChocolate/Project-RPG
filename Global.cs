using Godot;
using System;

public class Global : Node
{
	//Universal behavior states
	public enum BehaviorState{
		STATE_IDLE,
		STATE_MOVE,
		STATE_DASH,
		STATE_ATTACK,
		STATE_ATTACKMOVE,
		STATE_DEATH,
		STATE_WANDER,
		STATE_CHASE
	}
	
	//Universal animation states
	//would like to make this an enum and have an AnimationStateToString function but everything in C# must be tied to a class and it's stupid
	public const string ANI_STATE_IDLE = "Idle";
	public const string ANI_STATE_MOVE = "Move";
	public const string ANI_STATE_ATTACK = "Attack";
	public const string ANI_STATE_ATTACKMOVE = "AttackMove";
	public const string ANI_STATE_CHANNEL = "Channel";
	public const string ANI_STATE_PASSIVE = "Passive";
	
	//Universal elemental types
	public const string ELEMENTALTYPE_FIRE = "Fire";
	public const string ELEMENTALTYPE_WATER = "Water";
	public const string ELEMENTALTYPE_EARTH = "Earth";
	public const string ELEMENTALTYPE_AIR = "Air";
	public const string ELEMENTALTYPE_THUNDER = "Thunder";
	public const string ELEMENTALTYPE_ICE = "Ice"; // for anything cold, not just water ice
	public const string ELEMENTALTYPE_LIGHT = "Light";
	public const string ELEMENTALTYPE_DARK = "Dark";
	public const string ELEMENTALTYPE_METAL = "Metal";
	public const string ELEMENTALTYPE_FORCE = "Force";
	
	//Groups
	public const string PlayerActivePassives = "PlayerActivePassives";
}
