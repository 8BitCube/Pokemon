using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Rioku Slayer
/// Date Modified: 5.12.2015
/// Definition:  TODO::  Rioku please explain this script
/// Modifacation: Andrew Mills
/// Reason:  Added little bit of commenting, also added a 'safty' to the OnTriggerEnter / OnTriggerExit Function (See Comments).
/// </summary>
public class Transporter : MonoBehaviour 
{
	public Transform otherSide;
	bool travel = true;

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
		//Only allow players to enter this trigger
		if(col.tag != WorldConstants.PLAYER_TAG)
			return;

		GetComponent<MeshRenderer>().enabled = true;
		if(col.tag == WorldConstants.PLAYER_TAG)
			travel = false;
	}

	void OnTriggerExit(Collider col)
	{
		//Only allow players to exit this trigger
		if(col.tag != WorldConstants.PLAYER_TAG)
			return;

		GetComponent<MeshRenderer>().enabled = false;
		if(col.tag == WorldConstants.PLAYER_TAG)
			travel=true;
	}

	/// <summary>
	/// Yields the transition from one position to the next
	/// </summary>
	/// <returns>The transition.</returns>
	public IEnumerator YieldTransition()
	{
		//Determine the features we wish to use
		IEnumerator fadeOutEffect = FadeManager.Instance.FadeScreen(FadeManager.FadeType.FadeOut);
		IEnumerator fadeOutMusic = FadeManager.Instance.FadeMusic(FadeManager.FadeType.FadeOut);

		IEnumerator fadeInEffect = FadeManager.Instance.FadeScreen(FadeManager.FadeType.FadeIn);
		IEnumerator fadeInMusic = FadeManager.Instance.FadeMusic(FadeManager.FadeType.FadeIn);

		//Fade out our features.
		while (fadeOutEffect.MoveNext() && fadeOutMusic.MoveNext())
		{ yield return null; }

		//Change position and change music
		GameManager.Instance.player.transform.position = otherSide.position;
		SoundManager.Instance.musicSource.GetComponent<AudioSource>().clip = nMusic.clip;
		SoundManager.Instance.musicSource.GetComponent<AudioSource>().Play ();

		//Simulate loading - Why you asking, because of visual effects
		yield return new WaitForSeconds (1);

		//Fade back in the features.
		while (fadeInEffect.MoveNext() && fadeInMusic.MoveNext())
		{ yield return null; }

		yield return null;
	}
}
