using Godot;
using System;

public class PlayerDetectionZone : Area2D
{
	public Player player = null;
	
	public bool CanSeePlayer(){
		return (player != null);
	}
	
	private void _on_PlayerDetectionZone_body_entered(object body){
		player = (Player)body;
	}
	
	private void _on_PlayerDetectionZone_body_exited(object body){
		player = null;
	}
}
