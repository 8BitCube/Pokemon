using UnityEngine;
using System.Collections;

public class RotateToCamera : MonoBehaviour 
{	 
	public bool isInverted = false;
	void LateUpdate () 
	{				
		if(Camera.main == null)
			return;

		var lookPos = -Camera.main.transform.forward;
		lookPos.y = 0;
		var rotation = Quaternion.LookRotation (lookPos * (isInverted ? -1: 1));
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, 500 * Time.deltaTime);
	}
}
