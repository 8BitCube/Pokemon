using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerParameters
{
	public float Speed = 3;
	public Texture2D SpriteSheet;
	public Vector3 SpriteSize = Vector3.one;
	public bool CanMove, CanJump, CanRun, CanBike, CanFish, CanFly, CanDive;
}
