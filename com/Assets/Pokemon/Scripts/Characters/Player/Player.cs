using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : PlayerBase
{
	public enum PlayerMovementType { Walking, Running, Biking, Fishing, Swimming, Diving }
	public PlayerMovementType curPlayerMovementType = PlayerMovementType.Walking;

	public CharacterController CharacterController;
	public Renderer SpriteRenderer;

	public PlayerParameters PlayerParameters { get; set; }	
	public PlayerParameters CurrentParameters;

	private void Start()
	{
		//Ignor collisions with other characters
		Physics.IgnoreLayerCollision (this.gameObject.layer, this.gameObject.layer);
		UpdateToNewParameters (PlayerMovementType.Biking);
	}

	public void Update()
	{
		if (PlayerMovement.MoveVector.x != 0 || PlayerMovement.MoveVector.z != 0) 
			PlayerVisuals.IsAnimating = true;
		else
			PlayerVisuals.IsAnimating = false;
	}

	public void UpdateToNewParameters(PlayerMovementType aPlayerMovementType)
	{
		curPlayerMovementType = aPlayerMovementType;

		switch(aPlayerMovementType)
		{			
		case PlayerMovementType.Walking:
			PlayerParameters = CurrentParameters;
			PlayerVisuals.CurSpriteState = PlayerVisuals.SpriteState.Walking;
			CurrentParameters.Speed = 3;
			CurrentParameters.CanMove = true;
			CurrentParameters.CanRun = true;
			CurrentParameters.CanJump = true;
			CurrentParameters.CanBike = true;
			break;
		case PlayerMovementType.Running:
			PlayerParameters = CurrentParameters;
			PlayerVisuals.CurSpriteState = PlayerVisuals.SpriteState.Running;
			CurrentParameters.Speed = 6;
			CurrentParameters.CanMove = true;
			CurrentParameters.CanRun = true;
			CurrentParameters.CanJump = true;
			CurrentParameters.CanBike = true;
			break;
		case PlayerMovementType.Biking:
			PlayerParameters = CurrentParameters;
			PlayerVisuals.CurSpriteState = PlayerVisuals.SpriteState.Biking;
			CurrentParameters.Speed = 10;
			CurrentParameters.CanMove = true;
			CurrentParameters.CanRun = false;
			CurrentParameters.CanJump = false;
			CurrentParameters.CanBike = true;
			break;
		case PlayerMovementType.Fishing:

			break;
		case PlayerMovementType.Swimming:
			break;
		case PlayerMovementType.Diving:
			break;
		}

		PlayerMovement.speed = CurrentParameters.Speed;
	}
}
