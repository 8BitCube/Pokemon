using UnityEngine;
using System.Collections;

public class Transporter : MonoBehaviour 
{
	public Transform otherSide;
	bool travel=true;
	public Transform transporting;

	public AudioInformation nMusic;
	public AudioClip doorSound;

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown (KeyCode.F) && !travel)
		{			
			SoundManager.Instance.PlaySFX(doorSound);
			StartCoroutine (YieldTransition());
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

	public IEnumerator YieldTransition()
	{
		IEnumerator fadeOutEffect = FadeManager.Instance.FadeScreen(FadeManager.FadeType.FadeOut);
		IEnumerator fadeOutMusic = FadeManager.Instance.FadeMusic(FadeManager.FadeType.FadeOut);

		IEnumerator fadeInEffect = FadeManager.Instance.FadeScreen(FadeManager.FadeType.FadeIn);
		IEnumerator fadeInMusic = FadeManager.Instance.FadeMusic(FadeManager.FadeType.FadeIn);

		while (fadeOutEffect.MoveNext() && fadeOutMusic.MoveNext())
		{ yield return null; }

		//Teleport and changed the music
		GameManager.Instance.Player.transform.position = otherSide.position;
		SoundManager.Instance.musicSource.GetComponent<AudioSource>().clip = nMusic.clip;
		SoundManager.Instance.musicSource.GetComponent<AudioSource>().Play ();

		//Simulate loading - Why you asking, because of testing.
		yield return new WaitForSeconds (1);

		while (fadeInEffect.MoveNext() && fadeInMusic.MoveNext())
		{ yield return null; }

		yield return null;
	}
}
