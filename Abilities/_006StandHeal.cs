using Godot;
using System;

//Stand still to heal every 4 seconds
public class _006StandHeal : Ability
{
	public Stats playerStats = null;
	public Timer timer = null;
	public Player player = null;
	
	public override void _Ready(){
		playerStats = (Stats)GetNode("/root/PlayerStats");
		timer = (Timer)GetNode("Timer");
		player = (Player)GetNode("/root/World/Objects/Player");
		AddToGroup(Global.PlayerActivePassives);
	}
	
	public override void _PhysicsProcess(float delta){
		if (timer.IsStopped() && !PlayerMoving()){
			timer.Start(4);
		}
		if (PlayerMoving()){
			timer.Stop();
		}
	}
	
	public bool PlayerMoving(){
		if (player.InputVector == new Vector2(0,0)){
			return false;
		}
		return true;
	}
	
	private void _on_Timer_timeout(){
		playerStats.Health += 1;
		timer.Start(4);
	}
}
