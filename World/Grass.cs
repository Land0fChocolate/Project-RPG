using Godot;
using System;

public class Grass : Entity
{
	private void _on_Hurtbox_area_entered(object area){
		createGrassEffect();
		QueueFree();
	}
	
	private void createGrassEffect(){
		PackedScene GrassEffect = (PackedScene)ResourceLoader.Load("res://Effects/GrassEffect.tscn");
		AnimatedSprite grassEffect = (AnimatedSprite)GrassEffect.Instance();
		Node world = GetTree().CurrentScene;
		world.AddChild(grassEffect);
		grassEffect.GlobalPosition = GlobalPosition;
	}
}
