using UnityEngine;
using System.Collections;

public class Transporter : MonoBehaviour {

	public Transform otherSide;
	bool travel=true;
	public Transform transporting;

	public SoundInfo nextMusic;
	public SoundInfo doorSound;

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown (KeyCode.F) && !travel)
		{			
			SoundManager.Instance.PlaySFX(doorSound);
			StartCoroutine (FadeManager.Instance.Fade(nextMusic, otherSide));
		}	
	}

	void OnTriggerEnter(Collider col)
	{
		GetComponent<MeshRenderer>().enabled=true;
		if(col.tag==WorldConstants.PLAYER_TAG)
		{
			transporting=col.transform;
			travel=false;
		}
	}

	void OnTriggerExit(Collider col)
	{
		GetComponent<MeshRenderer>().enabled=false;
		if(col.tag==WorldConstants.PLAYER_TAG)
		{
			travel=true;
		}
	}
}
