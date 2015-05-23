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
	public Slider musicVSlider, sfxVSlider, qualityLevelSlider, antiAliasSlider, anisotropicModeSlider, anisotropicLevelSlider;
	public Text musicVText, sfxVText, qualityText, antiAliasText, anisotropicModeText, anisotropicLevelText, fpsCounterText;
	
	public Toggle FPSToggle, windowedModeToggle, vSyncToggle;
	public Transform parentPanel;
	public GameObject resButtonPrefab;
	
	private Camera canvasCamera;
	private  Resolution[] resolutions;

	private bool setMenu, openMenu, showFPS, fullScreenMode, toggleVSync;
	
	private const float fpsMeasurePeriod = 0.2f;
	private float fpsNextPeriod = 0;
	private int fpsAccumulator = 0, currentFps, currentResIndex;

	public void Awake()
	{
		resolutions = Screen.resolutions;
		for(int x = 0; x < resolutions.Length; x++)
		{
			GameObject _btn = Instantiate(resButtonPrefab) as GameObject;
			_btn.GetComponentInChildren<Text>().text = resolutions[x].width + " x " + resolutions[x].height;
			int _index = x;
			_btn.GetComponent<Button>().onClick.AddListener(() => { SetWindowedMode (_index);});
			_btn.transform.SetParent(parentPanel, false);
		}
	}

	public void UpdateMusicSlider()	
	{ 
		SoundManager.Instance.musicSource.volume = musicVSlider.value;
		musicVText.text = (SoundManager.Instance.musicSource.volume * 100).ToString () + "%";
	}

	public void UpdateSFXSlider()	
	{
		SoundManager.Instance.sfxSource.volume = sfxVSlider.value;	 
		
		sfxVText.text = (SoundManager.Instance.sfxSource.volume * 100).ToString () + "%";
	}

	public void SetQuality() //changes the general Quality setting without changing the Vsync,Antialias or Anisotropic settings.
	{
		int graphicSetting=Mathf.RoundToInt(qualityLevelSlider.value);
		QualitySettings.SetQualityLevel(graphicSetting,true);
		qualityText.text=QualitySettings.names[graphicSetting];
		//keep settings the way the Sliders and Toggels are set.
		//SetWindowedMode();
		SetVSync();
		SetAntiAlias();
		SetAnisotropicFiltering();
		SetAnisotropicFilteringLevel();
	}

	public void SetWindowedMode(int aIndex)
	{
		fullScreenMode=!windowedModeToggle.isOn;	
		Screen.SetResolution(resolutions[aIndex].width, resolutions[aIndex].height, fullScreenMode);
	}
	
	public void SetVSync()
	{
		if (vSyncToggle.isOn)
			QualitySettings.vSyncCount=1;
		else QualitySettings.vSyncCount=0;
	}
	
	public void SetAntiAlias()
	{
		int sliderValue=Mathf.RoundToInt(antiAliasSlider.value);
		switch(sliderValue)
		{
		case 0:QualitySettings.antiAliasing=0;
			antiAliasText.text="Off";
			break;
		case 1:QualitySettings.antiAliasing=2;
			antiAliasText.text=QualitySettings.antiAliasing.ToString()+"x Multi Sampling";
			break;
		case 2:QualitySettings.antiAliasing=4;
			antiAliasText.text=QualitySettings.antiAliasing.ToString()+"x Multi Sampling";
			break;
		case 3:QualitySettings.antiAliasing=8;
			antiAliasText.text=QualitySettings.antiAliasing.ToString()+"x Multi Sampling";
			break;
		}
	}
	
	public void SetAnisotropicFiltering()
	{
		switch(Mathf.RoundToInt(anisotropicModeSlider.value))
		{
		case 0:QualitySettings.anisotropicFiltering=AnisotropicFiltering.Disable;
			anisotropicModeText.text="Disabled";
			break;
		case 1:QualitySettings.anisotropicFiltering=AnisotropicFiltering.Enable;
			anisotropicModeText.text="Enabled";
			break;
		case 2:QualitySettings.anisotropicFiltering=AnisotropicFiltering.ForceEnable;
			anisotropicModeText.text="ForceEnabled";
			break;
		}
	}
	
	public void SetAnisotropicFilteringLevel()
	{
		int SliderValue=Mathf.RoundToInt(anisotropicLevelSlider.value);
		Texture.SetGlobalAnisotropicFilteringLimits(SliderValue,SliderValue);
		anisotropicLevelText.text=SliderValue.ToString(); 
	}

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
