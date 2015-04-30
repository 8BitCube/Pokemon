using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FOV2DEyes : MonoBehaviour
{
	public bool raysGizmosEnabled;

	[Range (1,5)]
	public int quality = 4;
	public int fovAngle = 90;
	public float fovMaxDistance = 5;
	public LayerMask cullingMask;
	public List<RaycastHit> hits = new List<RaycastHit>();
	
	int numRays;
	float currentAngle;
	Vector3 direction;
	RaycastHit hit;

	/// <summary>
	/// Update this instance.
	/// </summary>
	private void Update()
	{
		CastRays();
	}

	/// <summary>
	/// Casts the rays.
	/// </summary>
	private void CastRays()
	{
		numRays = fovAngle * quality;
		currentAngle = fovAngle / -2;
		
		hits.Clear();
		
		for (int i = 0; i < numRays; i++)
		{
			direction = Quaternion.AngleAxis(currentAngle, transform.up) * transform.forward;
			hit = new RaycastHit();
			
			if(Physics.Raycast(transform.position, direction, out hit, fovMaxDistance, cullingMask) == false)
				hit.point = transform.position + (direction * fovMaxDistance);
			
			hits.Add(hit);

			currentAngle += 1f / quality;
		}
	}

	/// <summary>
	/// Raises the draw gizmos selected event.
	/// </summary>
	private void OnDrawGizmos()
	{
		if (raysGizmosEnabled && hits.Count() > 0) 
		{
			Gizmos.color = Color.white;
			for(int x = 0; x < hits.Count; x+=20)
			{
				Gizmos.DrawSphere(hits[x].point, 0.04f);
				Gizmos.DrawLine(transform.position, hits[x].point);
			}
		}
	}
	
}
