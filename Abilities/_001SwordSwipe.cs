using Godot;
using System;

public class _001SwordSwipe : Ability
{	
	public AnimationTree animationTree = null;
	private AnimationNodeStateMachinePlayback animationState;
	public HitboxSword hitbox;
	
	public override void _Ready(){
		animationTree = (AnimationTree)GetNode("AnimationTree");
		animationTree.Active = true;
		animationState = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		hitbox = (HitboxSword)GetNode("HitboxPivot/HitboxSword");
		hitbox.KnockbackVector = KnockbackVector * KnockbackPower;
		hitbox.Damage = AttackPower;
	}
	
	public override void _PhysicsProcess(float delta){
		animationState.Travel("Attack");
  	}
	
	private void abilityFinished(){
		QueueFree();
	}
}
