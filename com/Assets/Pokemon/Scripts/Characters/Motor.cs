using UnityEngine;
using System.Collections;

public class Motor : CharacterBase 
{
	public CharacterController Controller;
	public AudioClip jumpSound;

	public float speed = 3;
	private float m_RotationSpeed = 50;

	public Vector3 MoveVector { get; set; }
	public int RotationDirection { get; set; }
	public float VerticalVelocity { get; set; }
	
	public float gravity = 21.0f;
	public float jumpSpeed = 6.0f;
	public float terminalVelocity = 20.0f;
	public int rotationSpeed = 5;

	public void Start()
	{
		Controller = GetComponent<CharacterController> ();
	}

	void Update()
	{
		UpdatePosition();
		UpdateRotation();
	}

	/// <summary>
	/// Updates the position.
	/// </summary>
	private void UpdatePosition()
	{
		// Transform MoveVector to World Space
		MoveVector = transform.TransformDirection(MoveVector);
		
		// Normalize our MoveVector if Magnitude is > 1
		if (MoveVector.magnitude > 1)
			MoveVector = Vector3.Normalize(MoveVector);
				
		// Multiply MoveVector by MoveSpeed
		MoveVector *= speed;

		// Reapply Verticle Velocity to MoveVector.y
		MoveVector = new Vector3(MoveVector.x, VerticalVelocity, MoveVector.z);
		
		// Apply Gravity
		ApplyGravity();
		
		// Move the Character in World Space
		Controller.Move (MoveVector * Time.deltaTime);
	}

	/// <summary>
	/// Updates the rotation.
	/// </summary>
	Vector3 eulerAngleV3 = Vector3.zero;
	private void UpdateRotation()
	{
		eulerAngleV3.x = transform.eulerAngles.x;
		eulerAngleV3.y = transform.eulerAngles.y + (Movement.rotationSpeed * Movement.RotationDirection) * m_RotationSpeed * Time.deltaTime;
		eulerAngleV3.z = transform.eulerAngles.z;

		transform.eulerAngles = eulerAngleV3;	
	}

	/// <summary>
	/// Applies the gravity to this character.
	/// </summary>
	private void ApplyGravity()
	{
		//Make sure we are not exceeding out -Terminal Velocity
		if(MoveVector.y > -terminalVelocity)
			MoveVector = new Vector3(MoveVector.x, MoveVector.y - gravity * Time.deltaTime, MoveVector.z);
		
		if(Controller.isGrounded && MoveVector.y <  -1.0f)
			MoveVector = new Vector3(MoveVector.x, -1.0f, MoveVector.z);
	}
	
	/// <summary>
	/// Jump the character attached to this instance
	/// </summary>
	public bool Jump()
	{
		if(Controller.isGrounded)
		{
			VerticalVelocity = jumpSpeed;
			if(jumpSound)
				SoundManager.Instance.PlaySFX(jumpSound);

			return true;
		}
		return false;
	}

	/// <summary>
	/// Snaps the allign character with camera.
	/// </summary>
	public void SnapAllignCharacterWithCamera()
	{
		transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);	
	}
}
