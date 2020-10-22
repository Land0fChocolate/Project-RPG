using Godot;
using System;

public class Camera2D : Godot.Camera2D
{
	public override void _Ready(){
		Position2D topLeft = (Position2D)GetNode("Limits/TopLeft"), bottomRight = (Position2D)GetNode("Limits/BottomRight");
		LimitTop = (int)topLeft.Position.y;
		LimitLeft = (int)topLeft.Position.x;
		LimitRight = (int)bottomRight.Position.x;
		LimitBottom = (int)bottomRight.Position.y;
	}
}
