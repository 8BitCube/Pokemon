using UnityEngine;
using System.Collections;

public class PlayerInput : CharacterBase
{
	public LayerMask mouseClickMask;

	private bool m_NumLockToggle = false;
	private bool m_RunToggle = false;

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		//Reset values so we can handle input properly.  
		Movement.VerticalVelocity = Movement.MoveVector.y;
		Movement.MoveVector = Vector3.zero;
		Movement.RotationDirection = 0;

		m_RunToggle = false;

		if(GameManager.Instance.AllowPlayerMovementInput())
			GetLocomotionInput();

		HandleActionInput();
	}

	/// <summary>
	/// Gets the locomotion input.
	/// </summary>
	private void GetLocomotionInput()
	{
		if(Character.CurrentParameters.CanRun)
		{
			// Toggle running Shift = true
			if(Input.GetKey(KeyCode.LeftShift) && Movement.Controller.isGrounded)
				m_RunToggle = true;

			if(m_RunToggle == true)
			{
				Character.CurrentParameters = Character.RunParameter;
				Character.UpdateParameter();
			}
			else
			{
				Character.CurrentParameters = Character.WalkParameter;
				Character.UpdateParameter();
			}
		}



		// Toggable Numlock
		if(Input.GetKeyDown(KeyCode.Numlock))
			m_NumLockToggle = !m_NumLockToggle;
		
		if(m_NumLockToggle == true)
			Movement.MoveVector += Vector3.forward;

		// If both left and right buttons are clicked, then move forward
		if(Input.GetMouseButton(0) && Input.GetMouseButton(1))
		{
			if(m_NumLockToggle == true)
				m_NumLockToggle = !m_NumLockToggle;
			
			Movement.MoveVector += Vector3.forward;
		}
		
		if(Input.GetMouseButton(1))
			Movement.SnapAllignCharacterWithCamera();

		if(Input.GetKey(KeyBindings.Forward) || Input.GetKey(KeyCode.UpArrow))
		{
			if(m_NumLockToggle == true)
				m_NumLockToggle = !m_NumLockToggle;
			Movement.MoveVector += Vector3.forward;
		}

		//If out state allows us to Jump, proceed
		if(Character.CurrentParameters.CanJump )
		{
			if(Input.GetKey(KeyBindings.Jump))
				Movement.Jump();
		}

		//If our state allows reverse movement, proceed
		if(Character.CurrentParameters.CanReverse)
		{
			if(Input.GetKey(KeyBindings.Backward) || Input.GetKey(KeyCode.DownArrow))
			{
				if(m_NumLockToggle == true)
					m_NumLockToggle = !m_NumLockToggle;
				Movement.MoveVector += -Vector3.forward;
			}
		}

		// Allow Strafing only when we walk
		if(Character.CurrentParameters.CanStraf)
		{
			if(Input.GetKey(KeyBindings.StrafLeft))
				Movement.MoveVector += Vector3.left;

			if(Input.GetKey(KeyBindings.StrafRight))
				Movement.MoveVector += Vector3.right;
		}

		// Allow Strafing only when we walk
		if(Character.CurrentParameters.CanRotate)
		{
			if(Input.GetKey(KeyBindings.Left) || Input.GetKey(KeyCode.LeftArrow))
				Movement.RotationDirection = -1;
			else if(Input.GetKey(KeyBindings.Right) || Input.GetKey(KeyCode.RightArrow))
				Movement.RotationDirection = 1;
		}
	}

	/// <summary>
	/// Handles the action input.
	/// </summary>
	private void HandleActionInput()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
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

		}
	}
}
