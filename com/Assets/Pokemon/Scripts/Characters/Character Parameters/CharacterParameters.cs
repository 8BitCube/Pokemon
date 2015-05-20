using UnityEngine;
using System.Collections;

public class CharacterParameters : ScriptableObject
{
	public string Name;
	public Texture2D SpriteSheet;
	
	public int Speed;
	public bool CanForward, CanStraf, CanReverse, CanRotate, CanJump, CanRun, CanBike, CanSwim, CanFish, CanFly, CanDive;
}
