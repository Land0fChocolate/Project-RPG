using Godot;
using System;

public class Effect : AnimatedSprite
{
	public override void _Ready(){
		Connect("animation_finished", this, "_on_animation_finished");
		Frame = 0;
		Play("Animate");
	}

	private void _on_animation_finished(){
		QueueFree();
	}
}
