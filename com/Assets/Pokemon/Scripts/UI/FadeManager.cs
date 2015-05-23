using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.12.2015
/// Definition:  This script will handle fading effects.  It can visually create a smooth transition to black/white screen
/// 			 and/or gradually fade in/out the music.
/// 
/// 			 To implement this feature please see the implementation section of the readMe file. (SOON TO COME).
/// </summary>
public class FadeManager : MonoBehaviour 
{
	public static FadeManager Instance = null;
	public enum FadeType { FadeIn, FadeOut }

	// Preferably a range from 0-200, this will adjust how fast we transition or fading.
	// NOTE::  200 does not mean instant transition
	[Range (0,200)] public int fadeTransitionSpeed = 25;

	//TODO::  Have a Save and Load feature for player to adjust the fadeTranitionSpeed.
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

		//Turn on the fadeEffect.
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

		//Turn it off if we are fading out.
		UIElementStatic.FadeEffect.gameObject.SetActive ((aFadeType == FadeType.FadeIn ) ? false : true);

		//Once completed exit coroutine
		yield return null;
	}

	/// <summary>
	/// Fades the objects renderer alpha value that is passed in.
	/// </summary>
	/// <returns>Null</returns>
	/// <param name="aFadeType">A fade type.</param>
	public IEnumerator FadeObject(FadeType aFadeType, Renderer renderer)
	{
		float _alphaFadeValue = (aFadeType == FadeType.FadeIn ) ? 1.0f : 0.0f;
		
		//Turn on the fadeEffect.
		UIElementStatic.FadeEffect.gameObject.SetActive (true);
		
		//Fade effect, loop until complete
		while((aFadeType == FadeType.FadeIn ) ? (_alphaFadeValue > 0.0f) : ( _alphaFadeValue < 1.0f))
		{			
			_alphaFadeValue += ((aFadeType == FadeType.FadeIn) ? -1 : 1 ) * (1 * (float)fadeTransitionSpeed / 100.0f) * Time.deltaTime;
			Color c = renderer.material.color;
			c.a = _alphaFadeValue;
			renderer.material.color = c;
			yield return null;
		}

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
		//Here we make sure that we have access to the globalData file, this way we can referance the saved music volume, 
		// if we do not have a reference, just set the volume to the default sound volume
		float currentSoundLevel = (aFadeType == FadeManager.FadeType.FadeIn) 
								? ((DataManager.globalData != null) 
		                        	? DataManager.globalData.MusicVolume : WorldConstants.DEFAULT_MUSIC_V)
									: SoundManager.Instance.musicSource.volume;

		float _soundValue = (aFadeType == FadeType.FadeIn ) ? 0.0f : 1.0f;

		while(((aFadeType == FadeType.FadeIn ) ? (_soundValue < 1.0f) : ( _soundValue > 0.0f)))
		{
			_soundValue += ((aFadeType == FadeType.FadeIn) ? 1 : -1 ) * (1 * (float)fadeTransitionSpeed / 100.0f) * Time.deltaTime;
			SoundManager.Instance.musicSource.volume = currentSoundLevel * _soundValue;
			yield return null;
		}

		//Once completed exit coroutine
		yield return null;
	}
}
