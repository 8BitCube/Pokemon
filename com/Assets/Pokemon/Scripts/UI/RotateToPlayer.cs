using UnityEngine;
using System.Collections;

public class RotateToPlayer : MonoBehaviour 
{	 
	public bool isInverted = false;
	void LateUpdate () 
	{						
		var lookPos = WorldManager.Instance.Player.transform.position - transform.position;
		lookPos.y = 0;
		var rotation = Quaternion.LookRotation (lookPos * (isInverted ? -1: 1));
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, 500 * Time.deltaTime);
	}
}
