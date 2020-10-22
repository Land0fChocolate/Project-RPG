using Godot;
using System;

public class _003Thing : Ability
{
	public Timer timer = null;
	
	public override void _Ready(){
		timer = (Timer)GetNode("Timer");
		timer.Start(3);
	}
	
	public override void _PhysicsProcess(float delta){
		MoveAndSlide(KnockbackVector * 50);
  	}
	
	private void _on_HitboxThing_area_entered(object area){
		QueueFree();
	}
	
	private void _on_Timer_timeout(){
		QueueFree();
	}
}
