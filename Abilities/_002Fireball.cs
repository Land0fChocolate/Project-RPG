using Godot;
using System;

public class _002Fireball : Ability
{	
	public Hitbox hitbox;
	private PackedScene ExplosionEffect = (PackedScene)ResourceLoader.Load("res://Effects/FireballExplosionEffect.tscn");
	
	public override void _Ready(){
		hitbox = (HitboxFireball)GetNode("HitboxPivot/HitboxFireball");
		hitbox.KnockbackVector = KnockbackVector * KnockbackPower;
		hitbox.Damage = AttackPower;
	}
	
	public override void _PhysicsProcess(float delta){
		MoveAndSlide(KnockbackVector * 200);
  	}
	
	private void _on_HitboxFireball_area_entered(object area){
		createExplosionEffect();
		abilityFinished();
	}
	
	private void createExplosionEffect(){
		AnimatedSprite explosionEffect = (AnimatedSprite)ExplosionEffect.Instance();
		Node world = GetTree().CurrentScene;
		world.AddChild(explosionEffect);
		explosionEffect.GlobalPosition = GlobalPosition;
	}
	
	private void abilityFinished(){
		QueueFree();
	}
}
