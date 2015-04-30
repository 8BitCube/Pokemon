using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour 
{
	public Transform Follower;
	
	//Follower Move
	private Quaternion wantedRotation;
	private Quaternion currentRotation;
	private float wantedRotationAngle;
	private float currentRotationAngle;
	private Vector3 wantedPosition;
	
	public float bondDistance = 2;
	public float bondDamping = 3;
		
	// Update is called once per frame
	void Update () 
	{
		UpdatePosition(Follower);
	}

	private Transform tempTarget;
	
	/// <summary>
	/// Updates the position.
	/// </summary>
	private void UpdatePosition(Transform aTarget)
	{
		// Calculate the current rotation angles
		wantedRotationAngle = aTarget.eulerAngles.y;
		currentRotationAngle = this.transform.eulerAngles.y;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, bondDamping * Time.deltaTime);
		// Convert the angle into a rotation
		currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		// Set the position of the camera on the x-z plane to:
		// bondDistance meters behind the prevFollower

		if(Vector3.Distance(this.transform.position, aTarget.position) > 1)
			this.GetComponent<Motor>().MoveVector = Vector3.forward;
		else
			this.GetComponent<Motor>().MoveVector = Vector3.zero;

		Vector3 targetPostition = new Vector3( aTarget.position.x, 
		                                      this.transform.position.y, 
		                                      aTarget.position.z );

		this.transform.LookAt( targetPostition ) ;

	}
}
