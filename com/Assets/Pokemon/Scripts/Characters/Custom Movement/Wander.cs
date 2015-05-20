using UnityEngine;
using System.Collections;

public class Wander : CharacterBase 
{
	private bool isMoving = false;

	private Vector3 circleCenter = Vector3.zero;
	private uint circleRadius   = 5;
	private float minDistance = 0.2f;
	
	//Pause Variables
	private float pauseTimer = 0.0f;
	private float pauseTime  = 0.0f;

	private Vector3 currentTargetPosition;

	public void Start() 
	{ 		
		circleCenter = transform.position;
		currentTargetPosition = circleCenter + (OnUnitCircle() * (circleRadius - Random.Range(0, circleRadius + 1)));

		transform.LookAt (currentTargetPosition);
	}

	public void Update()
	{
		if(Vector3.Distance(this.transform.position, currentTargetPosition) < minDistance)
		{
			currentTargetPosition = circleCenter + (OnUnitCircle() * (circleRadius - Random.Range(0, circleRadius + 1)));

			pauseTime = Random.Range(1, 5);
			isMoving = false;
		}

		if(!isMoving)
			Pause();
		else
		{
			transform.LookAt (currentTargetPosition);
			Movement.MoveVector = Vector3.forward;
		}
	}

	/// <summary>
	/// Pause this instance's movement.
	/// </summary>
	private void Pause()
	{
		//Increment the pauseTimer to match the deltaTime
		pauseTimer += Time.deltaTime;
		Movement.MoveVector = Vector3.zero;
		//When we cap the PauseTimer, reset the value, and change the VelocityState
		if(pauseTimer > pauseTime)
		{
			pauseTimer = 0.0f;
			isMoving = true;
		}
	}
	
	private Vector3 OnUnitCircle ()
	{
		float _angleInRadians = Random.Range(0, 2 * Mathf.PI);
		float _x = Mathf.Cos(_angleInRadians);
		float _z = Mathf.Sin(_angleInRadians);
		
		//Create a V3 based on a 2D plane
		return new Vector3(_x, 0, _z);
	}
}
