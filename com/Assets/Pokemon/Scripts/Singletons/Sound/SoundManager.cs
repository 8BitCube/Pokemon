using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{
	public static SoundManager Instance = null;     //Allows other scripts to call functions from SoundManager. 

	public AudioInformation music;
	public AudioInformation sfx;

	public AudioSource musicSource;
	public AudioSource sfxSource;

	void Awake ()
	{
		//Check if there is already an instance of SoundManager
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy (gameObject);

		DataManager.onLoad += this.Load;
		DataManager.onSave += this.Save;

		musicSource.GetComponent<AudioSource>().clip = music.clip;
		musicSource.GetComponent<AudioSource>().Play();
	}

	public void OnDestroy()
	{
		DataManager.onLoad -= this.Load;
		DataManager.onSave -= this.Save;
	}

	public void Load()
	{
		musicSource.volume = DataManager.globalData.MusicVolume;
		sfxSource.volume = DataManager.globalData.SFXVolume;
	}
	
	public void Save()
	{
		DataManager.globalData.MusicVolume = musicSource.volume;
		DataManager.globalData.SFXVolume = sfxSource.volume;
	}

	void LateUpdate()
	{
		if(music.IsLooped)
		{
			if(musicSource.GetComponent<AudioSource>().timeSamples >= music.NumSamples)
			{
				musicSource.GetComponent<AudioSource>().timeSamples = music.LoopStartSample;
				musicSource.GetComponent<AudioSource>().Play();
			}
		}
	}

	public void PlaySFX(AudioClip aSFX)
	{
		sfxSource.GetComponent<AudioSource>().Stop();
		sfxSource.GetComponent<AudioSource>().clip = aSFX;
		sfxSource.GetComponent<AudioSource>().Play ();
	}
}