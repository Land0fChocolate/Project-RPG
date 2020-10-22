using Godot;
using System;

public class WanderController : Node2D
{
	[Export] public int WanderRange = 32;
	public Vector2 startPosition = new Vector2(), TargetPosition = new Vector2(), targetVector = new Vector2();
	public Timer timer = null;
	
	public override void _Ready(){
		startPosition = GlobalPosition;
		TargetPosition = GlobalPosition;
		timer = (Timer)GetNode("Timer");
	}
	
	//for some reason, this doesn't work
	public void UpdateTargetPosition(){
		Random r = new Random();
		float rFloatX = (float)r.Next(-WanderRange, WanderRange), rFloatY = r.Next(-WanderRange, WanderRange);
		targetVector = new Vector2(rFloatX, rFloatY);
		TargetPosition = startPosition + targetVector;
	}
	
	public Vector2 NewTargetPosition(){
		Random r = new Random();
		float rFloatX = (float)r.Next(-WanderRange, WanderRange), rFloatY = r.Next(-WanderRange, WanderRange);
		targetVector = new Vector2(rFloatX, rFloatY);
		return startPosition + targetVector;
	}
	
	public float GetTimeLeft(){
		return timer.TimeLeft;
	}
	
	public void StartWanderTimer(float duration){
		timer.Start(duration);
	}
	
	private void _on_Timer_timeout(){
		UpdateTargetPosition();
	}
}
