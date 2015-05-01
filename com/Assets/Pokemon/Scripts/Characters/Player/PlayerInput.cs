using UnityEngine;
using System.Collections;

public class PlayerInput : PlayerBase
{
	public LayerMask mouseClickMask;

	private Interaction targetInteraction = null;
	private bool m_NumLockToggle = false;
	private bool m_RunToggle = false;
	private bool m_BikeRideToggle = false;

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		//Reset values so we can handle input properly.  
		PlayerMovement.VerticalVelocity = PlayerMovement.MoveVector.y;
		PlayerMovement.MoveVector = Vector3.zero;

		GetLocomotionInput();
		HandleActionInput();
	}

	/// <summary>
	/// Gets the locomotion input.
	/// </summary>
	private void GetLocomotionInput()
	{
		if(WorldManager.Instance.AllowInput == false)
			return;

		// Toggle the Biking
		if(Player.CharacterController.isGrounded && Input.GetKeyDown(KeyCode.Backspace))
			m_BikeRideToggle = !m_BikeRideToggle;
		
		// Toggle running Shift = true
		if(Input.GetKeyDown(KeyCode.LeftShift))
			m_RunToggle = true;
		if(Input.GetKeyUp(KeyCode.LeftShift))
			m_RunToggle = false;

		if(Player.PlayerParameters.CanRun)
		{
			if(m_RunToggle == true)
				Player.UpdateToNewParameters(Player.PlayerMovementType.Running);
		}

		if(Player.PlayerParameters.CanBike)
		{
			if(m_BikeRideToggle == true)
				Player.UpdateToNewParameters(Player.PlayerMovementType.Biking);
			else
			{
				if(m_RunToggle == false)
					Player.UpdateToNewParameters(Player.PlayerMovementType.Walking);
				else
					Player.UpdateToNewParameters(Player.PlayerMovementType.Running);
			}
		}

		//If we are not running or biking then walk
		if(m_RunToggle == false && m_BikeRideToggle == false)
			Player.UpdateToNewParameters(Player.PlayerMovementType.Walking);

		// Toggable Numlock
		if(Input.GetKeyDown(KeyCode.Numlock))
			m_NumLockToggle = !m_NumLockToggle;
		
		if(m_NumLockToggle == true)
			PlayerMovement.MoveVector += Vector3.forward;

		// If both left and right buttons are clicked, then move forward
		if(Input.GetMouseButton(0) && Input.GetMouseButton(1))
		{
			if(m_NumLockToggle == true)
				m_NumLockToggle = !m_NumLockToggle;
			
			PlayerMovement.MoveVector += Vector3.forward;
		}
		
		if(Input.GetMouseButton(1))
			PlayerMovement.SnapAllignCharacterWithCamera();


		if(Player.PlayerParameters.CanJump && Input.GetKey(KeyBindings.Jump))
			PlayerMovement.Jump();

		if(Input.GetKey(KeyBindings.Forward) || Input.GetKey(KeyCode.UpArrow))
		{
			if(m_NumLockToggle == true)
				m_NumLockToggle = !m_NumLockToggle;
			PlayerMovement.MoveVector += Vector3.forward;
		}

		if(Input.GetKey(KeyBindings.Backward) || Input.GetKey(KeyCode.DownArrow))
		{
			if(m_NumLockToggle == true)
				m_NumLockToggle = !m_NumLockToggle;
			PlayerMovement.MoveVector += -Vector3.forward;
		}

		// Allow Strafing only when we walk
		if(Player.curPlayerMovementType == Player.PlayerMovementType.Walking)
		{
			if(Input.GetKey(KeyBindings.StrafLeft))
				PlayerMovement.MoveVector += Vector3.left;

			if(Input.GetKey(KeyBindings.StrafRight))
				PlayerMovement.MoveVector += Vector3.right;
		}

		if(Input.GetKey(KeyBindings.Left) || Input.GetKey(KeyCode.LeftArrow))
			PlayerMovement.RotationDirection = -1;
		else if(Input.GetKey(KeyBindings.Right) || Input.GetKey(KeyCode.RightArrow))
			PlayerMovement.RotationDirection = 1;
		else
			PlayerMovement.RotationDirection = 0;

	}

	/// <summary>
	/// Handles the action input.
	/// </summary>
	private void HandleActionInput()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			//Turn off our old object
			if(targetInteraction != null)
				targetInteraction.Deactivate();
		}

		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			Target();
		}
	}

	private void Target()
	{
		RaycastHit _hitInfo = new RaycastHit();
		bool _hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hitInfo, Mathf.Infinity, mouseClickMask);
		
		if(_hit) 
		{
			//We cannot select ourself
			if(_hitInfo.collider.gameObject.GetComponent<Interaction>() == Player.GetComponent<Interaction>())
				return;
			
			//if we have clicked a new object
			if(targetInteraction != _hitInfo.collider.gameObject.GetComponent<Interaction>())
			{
				//Turn off our old object
				//if(targetInteraction != null)
					//targetInteraction.Deactivate();
			}
			
			targetInteraction = _hitInfo.collider.gameObject.GetComponent<Interaction>();
			targetInteraction.Activate();
		}
	}
}
