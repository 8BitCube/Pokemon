using UnityEngine;

public static class Helper 
{
	public struct ClipPlanePoints
	{
		public Vector3 UpperLeft;
		public Vector3 UpperRight;
		public Vector3 LowerLeft;
		public Vector3 LowerRight;
	}
	
	public static float ClampAngle(float angle, float min, float max)
	{
		do
		{
			if(angle < -360)
				angle += 360;
			if(angle > 360)
				angle -= 360;
		} 
		while (angle < -360 || angle > 360);
		
		return Mathf.Clamp(angle, min, max);
	}
	
	public static ClipPlanePoints ClipPlaneAtNear(Vector3 pos)
	{
		var _clipPlanePoint = new ClipPlanePoints();
		
		if(Camera.main == null)
			return _clipPlanePoint;
		
		var transform = Camera.main.transform;
		var halfFOV = (Camera.main.fieldOfView / 2) * Mathf.Deg2Rad;
		var aspect = Camera.main.aspect;
		var distance = Camera.main.nearClipPlane;
		var height = distance * Mathf.Tan (halfFOV);
		var width = height * aspect;
		
		// Move our point from pos to the right by the width
		_clipPlanePoint.LowerRight = pos + transform.right * width;
		_clipPlanePoint.LowerRight -= transform.up * height;
		_clipPlanePoint.LowerRight += transform.forward;
		
		_clipPlanePoint.LowerLeft = pos - transform.right * width;
		_clipPlanePoint.LowerLeft -= transform.up * height;
		_clipPlanePoint.LowerLeft += transform.forward;
		
		_clipPlanePoint.UpperRight = pos + transform.right * width;
		_clipPlanePoint.UpperRight += transform.up * height;
		_clipPlanePoint.UpperRight += transform.forward;
		
		_clipPlanePoint.UpperLeft = pos - transform.right * width;
		_clipPlanePoint.UpperLeft += transform.up * height;
		_clipPlanePoint.UpperLeft += transform.forward;
		
		
		return _clipPlanePoint;
	}
}
