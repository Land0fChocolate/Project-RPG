using Godot;
using System;

public class SoftCollision : Area2D
{
	public bool IsColliding(){
		return GetOverlappingAreas().Count > 0;
	}
	
	public Vector2 GetPushVector(){
		var pushVector = Vector2.Zero;
		if (IsColliding()){
			Godot.Collections.Array areasList = GetOverlappingAreas();
			var area = (Area2D)areasList[0];
			pushVector = area.GlobalPosition.DirectionTo(GlobalPosition);
			pushVector = pushVector.Normalized();
		}
		return pushVector;
	}
}
