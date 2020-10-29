using Godot;
using System;

public class Status_Stun : StatusEffect
{
	public Timer timer = null;
	
	public override void _Ready(){
		timer = (Timer)GetNode("Timer");
		timer.Start(Time);
		//AddToGroup([put something here]);
	}
	
	public override void _PhysicsProcess(float delta){
		if (timer.IsStopped()){
			QueueFree();
		}
		
		//TODO: How to get this to affect the object it hit?
	}

}
