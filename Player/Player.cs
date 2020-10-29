using Godot;
using System;
using System.Collections.Generic;

public class Player : KinematicBody2D
{
	public Global.BehaviorState state = Global.BehaviorState.STATE_MOVE;
	private float MoveSpeed = 100;
	private float DashSpeed = 200;
	public Stats playerStats = null;
	public AnimationPlayer blinkAnimationPlayer = null;
	public AnimationTree animationTree = null;
	private AnimationNodeStateMachinePlayback playerAnimationState;
	private PackedScene playerHurtSound = (PackedScene)ResourceLoader.Load("res://Player/PlayerHurtSound.tscn");
	
	public Vector2 DashVector = Vector2.Down, InputVector = new Vector2();
	private PackedScene[] WieldedAbilityScene = new PackedScene[6];
	private Ability[] WieldedAbility = new Ability[6];
	public List<Pin> PlayerPins = new List<Pin>();
	public Slot[][] SlotSets = new Slot[][]{};
	public int currentSlotSet = 0;
	public int firedPin = 0;
	
	public override void _Ready(){
		playerStats = (Stats)GetNode("/root/PlayerStats");
		playerStats.Connect("NoHealth", this, "queue_free");
		blinkAnimationPlayer = (AnimationPlayer)GetNode("BlinkAnimationPlayer");
		animationTree = (AnimationTree)GetNode("AnimationTree");
		animationTree.Active = true;
		playerAnimationState = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		
		//These pins are added to the player inventory temporarily for testing. They will be removed later.
		(string abilityAddress, string userAnimation, bool passive) = AbilityCatalog.SwordSwipe();
		PlayerPins.Add(new Pin{
			AbilityAddress = abilityAddress,
			UserAnimation = userAnimation,
			Passive = passive,
			Level = 1, 
			EXP = 0, 
			Quality = 0, 
			SlotNum = 1
		});
		
		(abilityAddress, userAnimation, passive) = AbilityCatalog.Fireball();
		PlayerPins.Add(new Pin{
			AbilityAddress = abilityAddress,
			UserAnimation = userAnimation,
			Passive = passive,
			Level = 1, 
			EXP = 0, 
			Quality = 0, 
			SlotNum = 2
		});
		
		(abilityAddress, userAnimation, passive) = AbilityCatalog.Thing();
		PlayerPins.Add(new Pin{
			AbilityAddress = abilityAddress,
			UserAnimation = userAnimation,
			Passive = passive,
			Level = 1, 
			EXP = 0, 
			Quality = 0, 
			SlotNum = 3
		});
		
		(abilityAddress, userAnimation, passive) = AbilityCatalog.StandHeal();
		PlayerPins.Add(new Pin{
			AbilityAddress = abilityAddress,
			UserAnimation = userAnimation,
			Passive = passive,
			Level = 1, 
			EXP = 0, 
			Quality = 0, 
			SlotNum = 6
		});
			
		SlotSets = BuildSlotSets(PlayerPins);
		LoadAbilities(SlotSets[currentSlotSet]);
	}

  public override void _PhysicsProcess(float delta){
	switch (state){
		case Global.BehaviorState.STATE_MOVE: 
			MoveState(SlotSets[currentSlotSet]);
			break;
		case Global.BehaviorState.STATE_DASH: 
			DashState();
			break;
		case Global.BehaviorState.STATE_ATTACK: 
			AttackState(SlotSets[currentSlotSet]);
			break;
		//TODO: other behavior states
	}
  }

	public void MoveState(Slot[] wieldedSlotSet){
		InputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		InputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		InputVector = InputVector.Normalized();
		if (InputVector != Vector2.Zero){
			DashVector = InputVector;
			
			animationTree.Set("parameters/Idle/blend_position", InputVector);
			animationTree.Set("parameters/Run/blend_position", InputVector);
			animationTree.Set("parameters/Attack/blend_position", InputVector);
			animationTree.Set("parameters/Dash/blend_position", InputVector);
			playerAnimationState.Travel("Run");
			MoveAndSlide(InputVector * MoveSpeed);
		} else {
			playerAnimationState.Travel("Idle");
			InputVector = MoveAndSlide(Vector2.Zero);
		}
		
		if (Input.IsActionJustPressed("ui_ability1")){
			if (wieldedSlotSet[0].AssignedPin != null && !wieldedSlotSet[0].AssignedPin.Passive){
				AbilityPrime(wieldedSlotSet, 1);
			}
		}
		
		if (Input.IsActionJustPressed("ui_ability2")){
			if (wieldedSlotSet[1].AssignedPin != null && !wieldedSlotSet[1].AssignedPin.Passive){
				AbilityPrime(wieldedSlotSet, 2);
			}
		}
		
		if (Input.IsActionJustPressed("ui_ability3")){
			if (wieldedSlotSet[2].AssignedPin != null && !wieldedSlotSet[2].AssignedPin.Passive){
				AbilityPrime(wieldedSlotSet, 3);
			}
		}
		
		if (Input.IsActionJustPressed("ui_ability4")){
			if (wieldedSlotSet[3].AssignedPin != null && !wieldedSlotSet[3].AssignedPin.Passive){
				AbilityPrime(wieldedSlotSet, 4);
			}
		}
		
		if (Input.IsActionJustPressed("ui_ability5")){
			if (wieldedSlotSet[4].AssignedPin != null && !wieldedSlotSet[4].AssignedPin.Passive){
				AbilityPrime(wieldedSlotSet, 5);
			}
		}
		
		if (Input.IsActionJustPressed("ui_ability6")){
			if (wieldedSlotSet[5].AssignedPin != null && !wieldedSlotSet[5].AssignedPin.Passive){
				AbilityPrime(wieldedSlotSet, 6);
			}
		}
		
		if (Input.IsActionJustPressed("ui_dash")){
			state = Global.BehaviorState.STATE_DASH;
		}
	}
	
	//NOTE: probably don't need this. Could just merge with AbilityExecute.
	public void AbilityPrime(Slot[] wieldedSlotSet, int firedPin){
		if (wieldedSlotSet[firedPin - 1].AssignedPin.Passive){
			return;
		}
		switch (wieldedSlotSet[firedPin - 1].AssignedPin.UserAnimation){
				case Global.ANI_STATE_ATTACK:
					state = Global.BehaviorState.STATE_ATTACK;
					AbilityExecute(wieldedSlotSet, firedPin);
					break;
				case Global.ANI_STATE_ATTACKMOVE:
					break;
		}
	}
	
	public void AbilityExecute(Slot[] wieldedSlotSet, int firedPin){
		WieldedAbility[firedPin - 1] = (Ability)WieldedAbilityScene[firedPin - 1].Instance();
		WieldedAbility[firedPin - 1].RotationDegrees = Mathf.Rad2Deg(DashVector.Angle()) - 90;
		WieldedAbility[firedPin - 1].KnockbackVector = DashVector;
		WieldedAbility[firedPin - 1].Level = wieldedSlotSet[firedPin-1].AssignedPin.Level;
		
		playerAnimationState.Travel(wieldedSlotSet[firedPin-1].AssignedPin.UserAnimation);
		
		Node world = GetTree().CurrentScene;
		world.AddChild(WieldedAbility[firedPin - 1]);
		WieldedAbility[firedPin - 1].GlobalPosition = GlobalPosition;
	}
	
	public void DashState(){
		MoveAndSlide(DashVector * DashSpeed);
		playerAnimationState.Travel("Dash");
	}
	
	public void AttackState(Slot[] wieldedSlotSet){
		playerAnimationState.Travel(wieldedSlotSet[0].AssignedPin.UserAnimation);
	}
	
	public void DashFinish(){
		state = Global.BehaviorState.STATE_MOVE;
	}
	
	public void AttackFinish(){
		state = Global.BehaviorState.STATE_MOVE;
	}
	
	private void _on_Hurtbox_area_entered(object area){
		playerStats.Health -= 1;
		AudioStreamPlayer playerHurtSounds = (AudioStreamPlayer)playerHurtSound.Instance();
		Node player = GetTree().CurrentScene;
		player.AddChild(playerHurtSounds);
		blinkAnimationPlayer.Play("Start");
	}
	
	//TODO: Slot stuff will need to be moved to an Inventory node. Then Player will need to preload it for use.
	public Slot[][] BuildSlotSets(List<Pin> PlayerPins){
		Slot[] slotSet1 = new Slot[6]{
			new Slot(1, null, 0, false),
			new Slot(2, null, 0, false),
			new Slot(3, null, 0, false),
			new Slot(4, null, 0, false),
			new Slot(5, null, 0, false),
			new Slot(6, null, 0, false),
		};
		Slot[] slotSet2 = new Slot[6]{
			new Slot(7, null, 0, false),
			new Slot(8, null, 0, false),
			new Slot(9, null, 0, false),
			new Slot(10, null, 0, false),
			new Slot(11, null, 0, false),
			new Slot(12, null, 0, false),
		};
		Slot[] slotSet3 = new Slot[6]{
			new Slot(13, null, 0, false),
			new Slot(14, null, 0, false),
			new Slot(15, null, 0, false),
			new Slot(16, null, 0, false),
			new Slot(17, null, 0, false),
			new Slot(18, null, 0, false),
		};
		
		foreach (Pin pin in PlayerPins){
			switch (pin.SlotNum){
				case 0: 
					break;
				case int n when (n <= 6):
					foreach (Slot slot in slotSet1){
						if (pin.SlotNum == slot.SlotNumber){
							slot.AssignedPin = pin;
						}
					}
					break;
				case int n when (n <= 12):
					foreach (Slot slot in slotSet2){
						if (pin.SlotNum == slot.SlotNumber){
							slot.AssignedPin = pin;
						}
					}
					break;
				case int n when (n <= 18):
					foreach (Slot slot in slotSet3){
						if (pin.SlotNum == slot.SlotNumber){
							slot.AssignedPin = pin;
						}
					}
					break;
			}
		}
		
		return new Slot[][]{slotSet1, slotSet2, slotSet3};
	}
	
	//TODO: LoadPinSet method. Includes Callgroup to remove previous PinSet passives.
	//GetTree().CallGroup(Global.PlayerActivePassives, QueueFree());
	
	//To be called whenever the player loads up a slotset to preload the abilities
	public void LoadAbilities(Slot[] wieldedSlotSet){
		for (int i = 0;i < 6;i++){
			if (wieldedSlotSet[i].AssignedPin != null){
				WieldedAbilityScene[i] = (PackedScene)ResourceLoader.Load(wieldedSlotSet[i].AssignedPin.AbilityAddress);
				if (wieldedSlotSet[i].AssignedPin.Passive){
					WieldedAbility[i] = (Ability)WieldedAbilityScene[i].Instance();
					Node world = GetTree().CurrentScene;
					world.CallDeferred("add_child", WieldedAbility[i]);
				}
			}
		}
	}
}

public class Slot : Node
{
	public int SlotNumber;
	public Pin AssignedPin;
	public float Cooldown;
	public bool Locked;
	
	public Slot(int SlotNumber, Pin AssignedPin, float Cooldown, bool Locked){
		this.SlotNumber = SlotNumber;
		this.AssignedPin = AssignedPin;
		this.Cooldown = Cooldown;
		this.Locked = Locked;
	}
}
