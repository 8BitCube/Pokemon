using UnityEngine;
using System.Collections;

public class SoundBox : MonoBehaviour 
{
	public AudioInformation nMusic;

	public void OnTriggerEnter(Collider col)
	{
		//Only allow players to enter this trigger
		if(col.tag != WorldConstants.PLAYER_TAG)
			return;

		//Is what we are current playing going to be the same as what we want to play, if so, exit
		if((SoundManager.Instance.musicSource.clip == nMusic.clip) && GameManager.Instance.IsSoundFading == false)
			return;

		//Are we already fading - check if the sound to be == to nmusic.clip - if so exit.
		if(GameManager.Instance.IsSoundFading && SoundManager.Instance.soundToBe == nMusic.clip)
			return;

		SoundManager.Instance.soundToBe = nMusic.clip;
		SoundManager.Instance.musicID = nMusic.ID;
		StartCoroutine (YieldTransition());
	}

	/// <summary>
	/// Yields the transition of one sound to another
	/// </summary>
	/// <returns>The transition.</returns>
	public IEnumerator YieldTransition()
	{
		//Determine the features we wish to use
		IEnumerator fadeOutMusic = FadeManager.Instance.FadeMusic(FadeManager.FadeType.FadeOut);
		IEnumerator fadeInMusic = FadeManager.Instance.FadeMusic(FadeManager.FadeType.FadeIn);

		GameManager.Instance.IsSoundFading = true;

		//Fade out our features.
		while (fadeOutMusic.MoveNext())
		{
			if (SoundManager.Instance.soundToBe != nMusic.clip)
				yield break;
			yield return null; 
		}


	
		//Change music
		SoundManager.Instance.musicSource.clip = nMusic.clip;
		SoundManager.Instance.musicSource.Play ();
				
		//Fade back in the features.
		while (fadeInMusic.MoveNext())
		{
			if (SoundManager.Instance.soundToBe != nMusic.clip)
				yield break;

			yield return null; 
		}

		GameManager.Instance.IsSoundFading = false;

		yield return null;
	}
}
