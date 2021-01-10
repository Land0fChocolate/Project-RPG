using Godot;
using System;

public class Status_Stun : StatusEffect
{
	public Timer timer = null;
	public int originalMaxSpeed;
	
	public override void _Ready(){
		//TODO: check to see if the target is a WorldObject. If true, then just QueueFree this instance immediately. 
		timer = (Timer)GetNode("Timer");
		originalMaxSpeed = Target.MaxSpeed;
		//AddToGroup([put something here]); //TODO: make a group for status effects for cleansing abilities.
		
		StunTarget();
	}
	
	public void StunTarget(){
		Target.MaxSpeed = 0;
		//TODO: create visual effect on the target while it is affected by the stun.
	}
	
	public void EndStatusEffect(){
		Target.MaxSpeed = originalMaxSpeed;
		QueueFree();
	}
	
	//TODO: what to do if the timer is cancelled (like when status effects are cleansed)?
	private void _on_Timer_timeout(){
		EndStatusEffect();
	}
}
