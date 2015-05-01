using UnityEngine;
using System.Collections;

/// <summary>
/// Fade manager.
/// Author: Andrew Mills
/// </summary>
public class FadeManager : MonoBehaviour 
{
	public static FadeManager Instance = null;

	[Range (0,100)]
	public int fadeTransitionSpeed = 25;

	void Awake()
	{
		if(Instance == null)
			Instance = this;
	}

	/// <summary>
	/// Fades the music and the screen.
	/// </summary>
	/// <returns>The music.</returns>
	/// <param name="aMusic">A music.</param>
	public IEnumerator Fade(SoundInfo aMusic)
	{
		float currentSoundLevel = SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume;

		//Fade our current music to 0 volume.
		while(SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume > 0f)
		{
			SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume -= (currentSoundLevel * (float)fadeTransitionSpeed / 100.0f) * Time.deltaTime;
			yield return null;
		}

		//Switch the audio clip and play it
		SoundManager.Instance.musicSource.GetComponent<AudioSource>().clip = aMusic.clip;
		SoundManager.Instance.musicSource.GetComponent<AudioSource>().Play ();
		
		//Start Fading Audio In to previous volume.
		while(SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume < currentSoundLevel)
		{
			SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume += (currentSoundLevel * (float)fadeTransitionSpeed / 100.0f) * Time.deltaTime;
			yield return null;
		}

		//Once completed exit coroutine
		yield return null;
	}

	/// <summary>
	/// Fades the music and the screen.
	/// </summary>
	/// <returns>The music.</returns>
	/// <param name="aMusic">A music.</param>
	public IEnumerator Fade(Transform newTransform)
	{
		float _alphaFadeValue = 0.0f;
		
		//Fade our current music to 0 volume.
		while(_alphaFadeValue < 1f)
		{
			_alphaFadeValue += (1 * (float)fadeTransitionSpeed / 100.0f) * Time.deltaTime;
			UIElementStatic.FadeEffect.color=new Color(UIElementStatic.FadeEffect.color.r,
			                                           UIElementStatic.FadeEffect.color.g,
			                                           UIElementStatic.FadeEffect.color.b, 
			                                           _alphaFadeValue);
			yield return null;
		}
		
		WorldManager.Instance.Player.transform.position = newTransform.position;
		WorldManager.Instance.AllowInput = true;

		//Start Fading Audio In to previous volume.
		while(_alphaFadeValue > 0f)
		{
			
			_alphaFadeValue -= (1 * (float)fadeTransitionSpeed / 100.0f) * Time.deltaTime;
			UIElementStatic.FadeEffect.color=new Color(UIElementStatic.FadeEffect.color.r,
			                                           UIElementStatic.FadeEffect.color.g,
			                                           UIElementStatic.FadeEffect.color.b, 
			                                           _alphaFadeValue);
			yield return null;
		}

		//Once completed exit coroutine
		yield return null;
	}

	/// <summary>
	/// Fades the music and the screen.
	/// </summary>
	/// <returns>The music.</returns>
	/// <param name="aMusic">A music.</param>
	public IEnumerator Fade(SoundInfo aMusic, Transform newTransform)
	{
		float currentSoundLevel = SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume;
		float _alphaFadeValue = 0.0f;
		
		//Fade our current music to 0 volume and fade out to black.
		while(SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume > 0f)
		{
			SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume -= (currentSoundLevel * (float)fadeTransitionSpeed / 100.0f) * Time.deltaTime;
			_alphaFadeValue += (1 * (float)fadeTransitionSpeed / 100.0f) * Time.deltaTime;
			UIElementStatic.FadeEffect.color=new Color(UIElementStatic.FadeEffect.color.r,
			                                           UIElementStatic.FadeEffect.color.g,
			                                           UIElementStatic.FadeEffect.color.b, 
			                                           _alphaFadeValue);
			yield return null;
		}
		
		WorldManager.Instance.Player.transform.position = newTransform.position;		
		WorldManager.Instance.AllowInput = true;
		
		//Switch the audio clip and play it
		SoundManager.Instance.musicSource.GetComponent<AudioSource>().clip = aMusic.clip;
		SoundManager.Instance.musicSource.GetComponent<AudioSource>().Play ();
		
		//Start Fading Audio In to previous volume and fade in to 0 alpha.
		while(SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume < currentSoundLevel)
		{
			
			SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume += (currentSoundLevel * (float)fadeTransitionSpeed / 100.0f) * Time.deltaTime;
			_alphaFadeValue -= (1 * (float)fadeTransitionSpeed / 100.0f) * Time.deltaTime;
			UIElementStatic.FadeEffect.color=new Color(UIElementStatic.FadeEffect.color.r,
			                                           UIElementStatic.FadeEffect.color.g,
			                                           UIElementStatic.FadeEffect.color.b, 
			                                           _alphaFadeValue);
			yield return null;
		}

		//Once completed exit coroutine
		yield return null;
	}
}
