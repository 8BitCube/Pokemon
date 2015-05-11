using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.10.2015
/// Definition:  
/// </summary>
public class FadeManager : MonoBehaviour 
{
	public static FadeManager Instance = null;
	public enum FadeType { FadeIn, FadeOut }

	[Range (0,100)]
	public int fadeTransitionSpeed = 25;

	void Awake()
	{
		if(Instance == null)
			Instance = this;
	}

	/// <summary>
	/// Fades the screen.
	/// </summary>
	/// <returns>The screen.</returns>
	/// <param name="aFadeType">A fade type.</param>
	public IEnumerator FadeScreen(FadeType aFadeType)
	{
		float _alphaFadeValue = (aFadeType == FadeType.FadeIn ) ? 1.0f : 0.0f;
		UIElementStatic.FadeEffect.gameObject.SetActive (true);

		//Fade effect, loop until complete
		while((aFadeType == FadeType.FadeIn ) ? (_alphaFadeValue > 0.0f) : ( _alphaFadeValue < 1.0f))
		{			
			_alphaFadeValue += ((aFadeType == FadeType.FadeIn) ? -1 : 1 ) * (1 * (float)fadeTransitionSpeed / 100.0f) * Time.deltaTime;
			UIElementStatic.FadeEffect.color=new Color(UIElementStatic.FadeEffect.color.r,
			                                           UIElementStatic.FadeEffect.color.g,
			                                           UIElementStatic.FadeEffect.color.b, 
			                                           _alphaFadeValue);
			yield return null;
		}
		
		UIElementStatic.FadeEffect.gameObject.SetActive ((aFadeType == FadeType.FadeIn ) ? false : true);

		//Once completed exit coroutine
		yield return null;
	}

	/// <summary>
	/// Fades the music.
	/// </summary>
	/// <returns>The music.</returns>
	/// <param name="aFadeType">A fade type.</param>
	public IEnumerator FadeMusic(FadeType aFadeType)
	{
		float currentSoundLevel = (aFadeType == FadeManager.FadeType.FadeIn) ? ((DataManager.globalData != null) ? DataManager.globalData.MusicVolume : 0.5f)
			: SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume;
		
		//Fade our current music to 0 volume.
		while((aFadeType == FadeManager.FadeType.FadeIn) ? (SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume < ((DataManager.globalData != null) ? DataManager.globalData.MusicVolume : 0.5f)) 
		      : (SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume > 0f))
		{
			SoundManager.Instance.musicSource.GetComponent<AudioSource>().volume -= (((aFadeType == FadeManager.FadeType.FadeIn) ? -1 : 1) * 
				(currentSoundLevel * (float)fadeTransitionSpeed / 100.0f) * Time.deltaTime);
			yield return null;
		}
		
		//Once completed exit coroutine
		yield return null;
	}
}
