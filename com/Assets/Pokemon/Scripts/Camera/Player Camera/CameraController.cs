using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform TargetLookAt;
	public Camera myCamera;
	public float Distance = 3.5f;
	public float DistanceMin = 3.0f;
	public float DistanceMax = 10.0f;
	public float DistanceSmooth = 0.5f;
	public float DistanceResumeSmooth = 0.3f;
	public float X_InputSensitivity = 5.0f;
	public float Y_InputSensitivity = 5.0f;
	public float MouseWheelSensitivity = 10.0f;
	public float X_Smooth = 0.05f;
	public float Y_Smooth = 0.1f;
	public float Y_MinLimit = -40.0f;
	public float Y_MaxLimit = 80.0f;
	public float OcclusionDistanceStep = 0.5f;
	public int MaxOcclusionChecks = 10;
	
	[SerializeField]
	private LayerMask mask = 0;
	private float inputX = 0.0f;
	private float inputY = 0.0f;
	private float velX = 0.0f;
	private float velY = 0.0f;
	private float velZ = 0.0f;
	private float velDistance = 0.0f;
	private float startDistance = 0.0f;
	private Vector3 position = Vector3.zero;
	private Vector3 desiredPosition = Vector3.zero;
	private float desiredDistance = 0.0f;
	private float distanceSmooth = 0.0f;
	private float preOccludedDistance = 0.0f;

	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start() 
	{
		myCamera = this.GetComponent<Camera> ();
		Distance = Mathf.Clamp(Distance, DistanceMin, DistanceMax);
		startDistance = Distance;
		Reset();
	}
	
	/// <summary>
	/// Lates the update.
	/// </summary>
	private void LateUpdate() 
	{
		if(TargetLookAt == null)
			return;
		
		HandlePlayerInput();
		
		var count = 0;
		do { CalculateDesiredPosition(); count ++; } 
		while(CheckIfOccluded(count));
		
		UpdatePosition();
	}
	
	/// <summary>
	/// Handles the player input for the Camera.
	/// </summary>
	private void HandlePlayerInput()
	{
		float _deadZone = 0.01f;

		if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
		{
			inputX += Input.GetAxis("Mouse X") * X_InputSensitivity;
			inputY -= Input.GetAxis("Mouse Y") * Y_InputSensitivity;
		}
		
		// This is where we will limit inputY
		inputY = Helper.ClampAngle (inputY, Y_MinLimit, Y_MaxLimit);
		
		if(Input.GetAxis("Mouse ScrollWheel") < -_deadZone || Input.GetAxis("Mouse ScrollWheel") > _deadZone)
		{
			desiredDistance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity,
			                              DistanceMin, DistanceMax);
			preOccludedDistance = desiredDistance;
			distanceSmooth = DistanceSmooth;
		}
	}
	
	/// <summary>
	/// Calculates the desired position.
	/// </summary>
	private void CalculateDesiredPosition()
	{
		// Evaluate Distance
		ResetDesiredDistance();
		Distance = Mathf.SmoothDamp(Distance, desiredDistance, ref velDistance, distanceSmooth);
		
		// Calculate desired position
		desiredPosition = CalculatePosition(inputY, inputX, Distance);
	}
	
	/// <summary>
	/// Calculates the position.
	/// </summary>
	/// <returns>The position.</returns>
	/// <param name="rotationX">Rotation x.</param>
	/// <param name="rotationY">Rotation y.</param>
	/// <param name="distance">Distance.</param>
	private Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
	{
		Vector3 _direction = new Vector3(0, 0, -distance);
		Quaternion _rotation = Quaternion.Euler (rotationX, rotationY, 0.0f); //CHECK HERE WHEN YOU GET HOME
		
		return TargetLookAt.position + _rotation * _direction;
	}
	
	/// <summary>
	/// Checks if occluded.
	/// </summary>
	/// <returns><c>true</c>, if if occluded was checked, <c>false</c> otherwise.</returns>
	/// <param name="count">Count.</param>
	private bool CheckIfOccluded(int count)
	{
		var _isOccluded = false;
		
		var nearestDistance = CheckCameraPoints(TargetLookAt.position, desiredPosition);
		
		if(nearestDistance != -1)
		{
			if(count < MaxOcclusionChecks)
			{
				_isOccluded = true;
				Distance -= OcclusionDistanceStep;
				
				if(Distance < 0.25f) //TEST THIS VALUE
					Distance = 0.25f;
			}
			else
				Distance = nearestDistance - this.myCamera.nearClipPlane;
			
			desiredDistance = Distance;
			distanceSmooth = DistanceResumeSmooth;
		}
		
		return _isOccluded;
	}
	
	/// <summary>
	/// Checks the camera points.
	/// </summary>
	/// <returns>The camera points.</returns>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	private float CheckCameraPoints(Vector3 from, Vector3 to)
	{
		var _nearestDistance = -1.0f;
		
		RaycastHit hitInfo;
		Helper.ClipPlanePoints clipPlanePoints = Helper.ClipPlaneAtNear(to);
		
		if(Physics.Linecast(from, clipPlanePoints.UpperLeft, out hitInfo, mask))
		{
			_nearestDistance = hitInfo.distance;
		}
		
		if(Physics.Linecast(from, clipPlanePoints.LowerLeft, out hitInfo, mask))
		{
			if(hitInfo.distance < _nearestDistance || _nearestDistance == -1) // Is it closer that previous nearDistance?
				_nearestDistance = hitInfo.distance;
		}
		
		if(Physics.Linecast(from, clipPlanePoints.UpperRight, out hitInfo, mask))
		{
			if(hitInfo.distance < _nearestDistance || _nearestDistance == -1) // Is it closer that previous nearDistance?
				_nearestDistance = hitInfo.distance;
		}
		if(Physics.Linecast(from, clipPlanePoints.LowerRight, out hitInfo, mask))
		{
			if(hitInfo.distance < _nearestDistance || _nearestDistance == -1) // Is it closer that previous nearDistance?
				_nearestDistance = hitInfo.distance;
		}
		if(Physics.Linecast(from, to + transform.forward * -myCamera.nearClipPlane, out hitInfo, mask))
		{
			if(hitInfo.distance < _nearestDistance || _nearestDistance == -1) // Is it closer that previous nearDistance?
				_nearestDistance = hitInfo.distance;
		}
		
		
		return _nearestDistance;	
	}
	
	/// <summary>
	/// Resets the desired distance.
	/// </summary>
	private void ResetDesiredDistance()
	{
		if (desiredDistance < preOccludedDistance)
		{
			var pos = CalculatePosition(inputY, inputX, preOccludedDistance);
			var nearestDistance = CheckCameraPoints(TargetLookAt.position, pos);
			
			if(nearestDistance == -1 || nearestDistance > preOccludedDistance)
			{
				desiredDistance = preOccludedDistance;
			}
		}
	}
	
	/// <summary>
	/// Updates the position.
	/// </summary>
	private void UpdatePosition()
	{
		var _posX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, X_Smooth);
		var _posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, Y_Smooth);
		var _posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, X_Smooth);
		
		position = new Vector3(_posX, _posY, _posZ);
		
		transform.position = position;
		transform.LookAt(TargetLookAt);
	}
	
	/// <summary>
	/// Reset this instance.
	/// </summary>
	private void Reset()
	{
		inputX = 0.0f;
		inputY = 10.0f;
		Distance = startDistance;
		desiredDistance = Distance;
		preOccludedDistance = Distance;
	}
}
