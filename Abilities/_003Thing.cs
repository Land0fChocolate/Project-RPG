using Godot;
using System;

public class _003Thing : Ability
{
	public Timer timer = null;
	public PackedScene StunScene = null;
	private StatusEffect Stun = null;
	
	public override void _Ready(){
		timer = (Timer)GetNode("Timer");
		timer.Start(3);
	}
	
	public override void _PhysicsProcess(float delta){
		MoveAndSlide(KnockbackVector * 50);
  	}
	
	private void _on_HitboxThing_area_entered(object area){
		StunScene = (PackedScene)ResourceLoader.Load("res://Abilities/Status_stun.tscn");
		Stun = (StatusEffect)StunScene.Instance();
		Stun.Time = 2;
		
		Node world = GetTree().CurrentScene;
		world.AddChild(Stun);
		
		QueueFree();
	}
	
	private void _on_Timer_timeout(){
		QueueFree();
	}
}
