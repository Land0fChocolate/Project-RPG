using Godot;
using System;

public class HealthUI : Control
{
	//public Label label = null;
	public Stats playerStats = null;
	public TextureRect heartUIFull = null;
	public TextureRect heartUIEmpty = null;
	public int hearts = 4;
	public int Hearts{
		get{
			return this.hearts;
		}
		set{
			setHearts(value);
		}
	}
	
	public int maxHearts = 4;
	public int MaxHearts{
		get{
			return this.maxHearts;
		}
		set{
			this.maxHearts = Mathf.Max(value, 1);
			if (heartUIFull != null){
				heartUIEmpty.Set("rect_size.x", hearts * 15);
			}
		}
	}
	
	public void setHearts(int value){
			this.hearts = Mathf.Clamp(value, 0, maxHearts);
//			if (label != null){
//				label.Text = "HP = " + hearts.ToString();
//			}
			if (heartUIFull != null){
				var rectSize = new Vector2();
				rectSize.x = hearts * 15;
				rectSize.y = 11;
				heartUIFull.Set("rect_size", rectSize);
			}
	}
	
	public override void _Ready(){
		//label = (Label)GetNode("Label");
		heartUIFull = (TextureRect)GetNode("HeartUIFull");
		heartUIEmpty = (TextureRect)GetNode("HeartUIEmpty");
		playerStats = (Stats)GetNode("/root/PlayerStats");
		this.MaxHearts = playerStats.MaxHealth;
		this.Hearts = playerStats.Health;
		playerStats.Connect("HealthChanged", this, "setHearts");
	}
}
