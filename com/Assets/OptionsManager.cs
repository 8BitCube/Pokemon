using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.13.2015
/// Definition:  Option Manager will hold functions that buttons as of unity 4.6+ can access via the button "On Click" event.
/// </summary>
public class OptionsManager : MonoBehaviour 
{	
	public Slider musicVSlider;
	public Slider sfxVSlider;

	public void UpdateMusicSlider()	{ SoundManager.Instance.musicSource.volume = musicVSlider.value; }
	public void UpdateSFXSlider()	{ SoundManager.Instance.sfxSource.volume = sfxVSlider.value;	 }

	public void ShowOptions()
	{
		musicVSlider.value = DataManager.globalData.MusicVolume;
		sfxVSlider.value = DataManager.globalData.SFXVolume;
	}

	public void ApplyOptionViaButton()
	{
		//Here we will save our options settings.
		DataManager.globalData.MusicVolume = musicVSlider.value;
		DataManager.globalData.SFXVolume = sfxVSlider.value;
		
		Serializer.Save<GlobalData>(Application.dataPath + WorldConstants.GLOBAL_INFO_DIR + WorldConstants.GLOBAL_INFO_FILE, DataManager.globalData);
		
		SoundManager.Instance.musicSource.volume = DataManager.globalData.MusicVolume;
		SoundManager.Instance.sfxSource.volume = DataManager.globalData.SFXVolume;
	}
	
	public void ExitOptionsViaButton()
	{
		SoundManager.Instance.musicSource.volume = DataManager.globalData.MusicVolume;
		SoundManager.Instance.sfxSource.volume = DataManager.globalData.SFXVolume;
	}
}
