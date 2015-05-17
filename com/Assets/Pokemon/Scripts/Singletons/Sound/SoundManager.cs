using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.12.2015
/// Definition:  This script will handle the constant music and single sound effects.
/// 		 	The music can be looped from one single clip, you must under stand <see cref="AudioInformation"/> for more details.
/// </summary>
public class SoundManager : MonoBehaviour 
{
	public static SoundManager Instance = null; 

	//Hold reference to our Audio information.
	//NOTE:  A SFX AudioInfo variable is not needed as it does not require any information other than the accual clip.
	public AudioInformation music;

	//Hold reference to our Audio Source Components.
	public AudioSource musicSource;
	public AudioSource sfxSource;

	void Awake ()
	{
		//Check if there is already an instance of SoundManager
		if (Instance == null)
			Instance = this;

		//Subscribe our save and load functions.
		DataManager.onSave += this.Save;
		DataManager.onLoad += this.Load;
	}

	public void OnDestroy()
	{
		//Unsubscribe our save and load functions
		DataManager.onSave -= this.Save;
		DataManager.onLoad -= this.Load;
	}

	/// <summary>
	/// Save the data we have adjusted.  This is a subscribed function, do not call this function.
	/// </summary>
	public void Save()
	{
		DataManager.globalData.MusicVolume = musicSource.volume;
		DataManager.globalData.SFXVolume = sfxSource.volume;
	}

	/// <summary>
	/// Load the data from our files.  This is a subscribed function, do not call this function.
	/// </summary>
	public void Load()
	{
		musicSource.volume  = DataManager.globalData.MusicVolume;
		sfxSource.volume 	= DataManager.globalData.SFXVolume;

		musicSource.clip = music.clip;
		musicSource.Play();
	}

	void Update()
	{
		if(music.IsLooped)
		{
			if( (!musicSource.isPlaying) || (musicSource.timeSamples >= music.NumSamples))
			{
				musicSource.timeSamples = music.LoopStartSample;
				musicSource.Play();
			}
		}
	}

	/// <summary>
	/// Plays the audio clip passed in
	/// </summary>
	/// <param name="aSFX">A SF.</param>
	public void PlaySFX(AudioClip aSFX)
	{
		sfxSource.Stop();
		sfxSource.clip = aSFX;
		sfxSource.Play ();
	}
}