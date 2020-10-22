using Godot;
using System;

public class Bat : NPC
{	
	public Stats stats = null;
	private PackedScene DeathEffect = (PackedScene)ResourceLoader.Load("res://Effects/EnemyDeathEffect.tscn");
	public AnimatedSprite batSprite = null;
	public AnimationPlayer animationPlayer = null;
	public SoftCollision softCollision = null;
	public WanderController wanderController = null;
	
	public override void _Ready(){
		stats = (Stats)GetNode("Stats");
		playerDetectionZone = (PlayerDetectionZone)GetNode("PlayerDetectionZone");
		animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
		batSprite = (AnimatedSprite)GetNode("AnimatedSprite");
		softCollision = (SoftCollision)GetNode("SoftCollision");
		wanderController = (WanderController)GetNode("WanderController");
		state = pickRandomState(new Global.BehaviorState[] {Global.BehaviorState.STATE_IDLE, Global.BehaviorState.STATE_WANDER});
	}

  	public override void _PhysicsProcess(float delta){
		Knockback = Knockback.MoveToward(Vector2.Zero, Friction * delta);
		Knockback = MoveAndSlide(Knockback);
		
		switch (state){
		case Global.BehaviorState.STATE_IDLE: 
			idleState(delta, Knockback);
			break;
		case Global.BehaviorState.STATE_WANDER: 
			wanderState();
			break;
		case Global.BehaviorState.STATE_CHASE: 
			chaseState();
			break;
		}
		
		if (softCollision.IsColliding()){
			Velocity += softCollision.GetPushVector() * 200;
		}
		MoveAndSlide(Velocity);
		batSprite.FlipH = Velocity.x < 0;
  	}
	
	private void idleState(float delta, Vector2 Knockback){
		Velocity = Velocity.MoveToward(Vector2.Zero, Friction * delta);
		seekPlayer();
		if (wanderController.GetTimeLeft() == 0){
			state = pickRandomState(new Global.BehaviorState[] {Global.BehaviorState.STATE_IDLE, Global.BehaviorState.STATE_WANDER});
			Random r = new Random();
			wanderController.StartWanderTimer(r.Next(1, 4));
		}
	}
	
	private void wanderState(){
		seekPlayer();
		NewTargetPosition = wanderController.NewTargetPosition();
		if (wanderController.GetTimeLeft() == 0){
			state = pickRandomState(new Global.BehaviorState[] {Global.BehaviorState.STATE_IDLE, Global.BehaviorState.STATE_WANDER});
			Random r = new Random();
			wanderController.StartWanderTimer(r.Next(1, 4));
			Direction = GlobalPosition.DirectionTo(NewTargetPosition);
		}
		
		Velocity = Velocity.MoveToward(Direction * MaxSpeed, Acceleration);
		if (GlobalPosition.DistanceTo(NewTargetPosition) <= 4){
			state = pickRandomState(new Global.BehaviorState[] {Global.BehaviorState.STATE_IDLE, Global.BehaviorState.STATE_WANDER});
			Random r = new Random();
			wanderController.StartWanderTimer(r.Next(1, 4));
		}
	}
	
	private void chaseState(){
		player = playerDetectionZone.player;
		if (player != null){
			Direction = GlobalPosition.DirectionTo(player.GlobalPosition);
			Velocity = Velocity.MoveToward(Direction * MaxSpeed, Acceleration);
		} else{
			state = pickRandomState(new Global.BehaviorState[] {Global.BehaviorState.STATE_IDLE, Global.BehaviorState.STATE_WANDER});
		}
	}
	
	private void seekPlayer(){
		if (playerDetectionZone.CanSeePlayer()){
			state = Global.BehaviorState.STATE_CHASE;
		}
	}
	
	private Global.BehaviorState pickRandomState(Global.BehaviorState[] statesList){
		Random rand = new Random();
		int randomElement = rand.Next(statesList.Length);
		return statesList[randomElement];
	}
	
	private void _on_Hurtbox_area_entered(object area){
		Hitbox hitbox = (Hitbox)area;
		Knockback = hitbox.KnockbackVector * 220;
		stats.Health -= hitbox.Damage;
		animationPlayer.Play("Start");
	}
	
	private void _on_Stats_NoHealth(){
		createDeathEffect();
		QueueFree();
	}
	
	private void createDeathEffect(){
		AnimatedSprite deathEffect = (AnimatedSprite)DeathEffect.Instance();
		Node world = GetTree().CurrentScene;
		world.AddChild(deathEffect);
		deathEffect.GlobalPosition = GlobalPosition;
	}
}
