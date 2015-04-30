using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{
	public static SoundManager Instance = null;     //Allows other scripts to call functions from SoundManager. 

	public SoundInfo music;
	public SoundInfo sfx;

	public AudioSource musicSource;
	public AudioSource sfxSource;

	public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
	public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

	void Awake ()
	{
		//Check if there is already an instance of SoundManager
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy (gameObject);

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);

		musicSource.GetComponent<AudioSource>().clip = music.clip;
		musicSource.GetComponent<AudioSource>().Play();
	}

	void Update()
	{
		if(music.isLooping)
		{
			if(musicSource.GetComponent<AudioSource>().timeSamples == music.numOfSamples)
			{
				musicSource.GetComponent<AudioSource>().timeSamples = 57344;
				musicSource.GetComponent<AudioSource>().Play();
			}
		}
	}

	public void PlaySFX(SoundInfo aSFX)
	{
		sfxSource.GetComponent<AudioSource>().Stop();
		sfxSource.GetComponent<AudioSource>().clip = aSFX.clip;
		sfxSource.GetComponent<AudioSource>().Play ();
	}
}