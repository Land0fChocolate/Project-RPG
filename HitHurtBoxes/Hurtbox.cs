using Godot;
using System;

public class Hurtbox : Area2D
{
	[Export] private bool showHitEffect = true;
	
	private void _on_Hurtbox_area_entered(object area){
		createHitEffect();
	}
	
	private void createHitEffect(){
		if (showHitEffect){
			PackedScene HitEffect = (PackedScene)ResourceLoader.Load("res://Effects/HitEffect.tscn");
			AnimatedSprite hitEffect = (AnimatedSprite)HitEffect.Instance();
			Node world = GetTree().CurrentScene;
			world.AddChild(hitEffect);
			hitEffect.GlobalPosition = GlobalPosition;
		}
	}
}
