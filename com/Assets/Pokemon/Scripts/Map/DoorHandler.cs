using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.17.2015
/// Definition:  The Door Handler will; fade out the screen + music, teleporting, then fade in again.
/// </summary>
public class DoorHandler : MonoBehaviour 
{
	public string DoorText;
	public Transform otherSide;

	public AudioInformation nMusic;
	public AudioClip doorSound;

	public TextMesh tMesh;

	// Update is called once per frame
	void Update () 
	{
		if(GameManager.Instance.AllowPlayerActionInput() && (Input.GetKeyDown (KeyCode.F) && tMesh.text != string.Empty))
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
		
		tMesh.text = DoorText;
	}

	void OnTriggerExit(Collider col)
	{
		//Only allow players to exit this trigger
		if(col.tag != WorldConstants.PLAYER_TAG)
			return;

		tMesh.text = string.Empty;
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

		GameManager.Instance.IsFading = true;

		//Fade out our features.
		while (fadeOutEffect.MoveNext() && fadeOutMusic.MoveNext())
		{ yield return null; }

		//Change position and change music
		GameManager.Instance.player.transform.position = otherSide.position;
		SoundManager.Instance.musicSource.clip = nMusic.clip;
		SoundManager.Instance.musicSource.Play ();

		//Simulate loading - Why you asking, because of visual effects
		yield return new WaitForSeconds (1);

		//Fade back in the features.
		while (fadeInEffect.MoveNext() && fadeInMusic.MoveNext())
		{ yield return null; }

		GameManager.Instance.IsFading = false;

		yield return null;
	}
}
