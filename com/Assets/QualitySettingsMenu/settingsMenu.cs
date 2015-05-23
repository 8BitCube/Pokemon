using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class settingsMenu : MonoBehaviour{

	public bool pauseTimeWhenMenuOpen;//if Checked in inspector - Sets TimeScale to 0 when menu is open.

	//if you use the prefab "_QualitySettingsMenu" they should all be assigned for you;
	public Slider qualityLevelSlider, antiAliasSlider, anisotropicModeSlider, anisotropicLevelSlider;
	public Text qualityText, antiAliasText, anisotropicModeText, anisotropicLevelText, fpsCounterText;
	public GameObject resolutionsPanel, resButtonPrefab;
	public Text currentResolutionText;
	public Toggle FPSToggle, windowedModeToggle, vSyncToggle;

	public GameObject menuTransform;
	private Camera canvasCamera;
	private  Resolution[] resolutions;

	private bool setMenu, openMenu, showFPS, fullScreenMode, toggleVSync;

	private const float fpsMeasurePeriod = 0.2f;
	private float fpsNextPeriod = 0;
	private int fpsAccumulator = 0, currentFps, wantedResX, wantedResY;

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(transform.gameObject);

		fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;

		//assigns the main camera to all canvasis that are not set to "Screen Space-Overlay".
		OnLevelWasLoaded();  

		//this reads all the values of the sliders and toggles and sets the Graphic settings accordingly.
		//(if the settings were saved before, they wil all be set to the saved setting before reading them)
		//(if this is the first time the game starts the toggles and sliders wil be where they were when the game was build)
		//(if you want the game to start at certain settings the first time, make sure to set everyting before you build)
		SetValues();
	}
	
	// Update is called once per frame
	void Update () {
					
		if (Input.GetKeyDown(KeyCode.Escape))//the Key used to open this menu.
		{
			//if you want to use a UI button to open the menu put this function on it.
			OpenQualitySettingMenu();
		}
		
		if (openMenu)
		{
			if (!setMenu)
			{
			    menuTransform.gameObject.SetActive(true); 
				setMenu=true;

				if (pauseTimeWhenMenuOpen)
					Time.timeScale=0;
			}
		}
		else 
		{
			if (!setMenu)
			{
                menuTransform.gameObject.SetActive(false);
				SavePlayerprefs();
				setMenu=true;

				if (pauseTimeWhenMenuOpen)
					Time.timeScale=1;
			}
		}

		//this FPScounter is a standard Unity asset (thought it was handy to put it in).
		if (showFPS)
		{
			fpsAccumulator++;
			if (Time.realtimeSinceStartup > fpsNextPeriod)
			{
				currentFps = (int) (fpsAccumulator/fpsMeasurePeriod);
				fpsAccumulator = 0;
				fpsNextPeriod += fpsMeasurePeriod;
			}
			fpsCounterText.text="FPS:"+currentFps;
		}
		else fpsCounterText.text="";
	}
	
	public void OpenQualitySettingMenu() //opens the menu.
	{
		openMenu=!openMenu;
		setMenu=false;
	}

	public void SetQuality() //changes the general Quality setting without changing the Vsync,Antialias or Anisotropic settings.
	{
		int graphicSetting=Mathf.RoundToInt(qualityLevelSlider.value);
		QualitySettings.SetQualityLevel(graphicSetting,true);
		qualityText.text=QualitySettings.names[graphicSetting];
		//keep settings the way the Sliders and Toggels are set.
		SetWindowedMode();
		SetVSync();
		SetAntiAlias();
		SetAnisotropicFiltering();
		SetAnisotropicFilteringLevel();
	}
	
	public void ShowFPS()
	{
		showFPS = !showFPS;
	}

	public void SetWindowedMode()
	{
		if (windowedModeToggle.isOn)
			fullScreenMode=false;		
		else fullScreenMode=true;
		Screen.SetResolution(wantedResX,wantedResY,fullScreenMode);
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

	public void SetShadows()
	{
		//not possible to acces in Unity at the moment i believe.
	}

	public void QuitGame()
	{
		SavePlayerprefs();
		Application.Quit();
	}

	private void SetValues()//set all settings according to the menu buttons.
	{
		//his reads how many Quality levels your "Game" has and sices the slider accordingly.
		qualityLevelSlider.maxValue=QualitySettings.names.Length-1;

		resolutions=Screen.resolutions;//checking the available resolution options.
		currentResolutionText.text=Screen.currentResolution.width+"x"+Screen.currentResolution.height;//sets the text of the Screen Resolution button to the res we start with.
		//filling the Screen Resolution option menu with buttons, one for every available resolution option your monitor has.
		for (int i=0; i<resolutions.Length; i++)
		{
			GameObject button=(GameObject)Instantiate(resButtonPrefab);//the button prefab.
			button.GetComponentInChildren<Text>().text=resolutions[i].width+"x"+resolutions[i].height;
			int index=i;
			button.GetComponent<Button>().onClick.AddListener(()=>{SetResolution(index);});//adding a "On click" SetResolution() function to the button.
			button.transform.SetParent(resolutionsPanel.transform,false);
		}

		LoadPlayerprefs(); // if any settings were saved before, this is where they are loaded and Sliders and toggles are set to the saved position.

		//reading Sliders and toggles and setting everything accordingly.
		int graphicSetting=Mathf.RoundToInt(qualityLevelSlider.value);
		QualitySettings.SetQualityLevel(graphicSetting,true);
		qualityText.text=QualitySettings.names[graphicSetting];
		SetVSync();
		SetWindowedMode();
		SetAntiAlias();
		SetAnisotropicFiltering();
		SetAnisotropicFilteringLevel();
	}

	public void SetResolution(int index)//the "On click" function on the resolutions buttons.
	{
		wantedResX=resolutions[index].width;
		wantedResY=resolutions[index].height;
		Screen.SetResolution(wantedResX,wantedResY,fullScreenMode);
		currentResolutionText.text=wantedResX+"x"+wantedResY;
	}

	public void ShowResolutionOptions()//opens the dropdown menu with available resolution options.
	{
		if (resolutionsPanel.activeSelf==false)
		resolutionsPanel.SetActive(true);
		else resolutionsPanel.SetActive(false);
	}

	private void SavePlayerprefs()
	{
		PlayerPrefs.SetInt("prefsSaved",1);

		PlayerPrefs.SetInt("graphicsSlider",Mathf.RoundToInt(qualityLevelSlider.value));
		PlayerPrefs.SetInt("antiAliasSlider",Mathf.RoundToInt(antiAliasSlider.value));
		PlayerPrefs.SetInt("anisotropicModeSlider",Mathf.RoundToInt(anisotropicModeSlider.value));
		PlayerPrefs.SetInt("anisotropicLevelSlider",Mathf.RoundToInt(anisotropicLevelSlider.value));

		PlayerPrefs.SetInt("wantedResolutionX",wantedResX);
		PlayerPrefs.SetInt("wantedResolutionY",wantedResY);

		int toggle = 0;
		if (!showFPS)
			toggle=0;
		else toggle=1;
		PlayerPrefs.SetInt("FPSToggle",toggle);

		if (vSyncToggle.isOn)
			toggle=1;
		else toggle=0;
		PlayerPrefs.SetInt("vSyncToggle",toggle);

		if (windowedModeToggle.isOn)
			toggle=1;
		else toggle=0;		
		PlayerPrefs.SetInt("windowedModeToggle",toggle);
	}

	private void LoadPlayerprefs()
	{
		if (PlayerPrefs.GetInt("prefsSaved")==1)//to check if there are any.
		{
			qualityLevelSlider.value=PlayerPrefs.GetInt("graphicsSlider");
		    antiAliasSlider.value=PlayerPrefs.GetInt("antiAliasSlider");
		    anisotropicModeSlider.value=PlayerPrefs.GetInt("anisotropicModeSlider");
		    anisotropicLevelSlider.value=PlayerPrefs.GetInt("anisotropicLevelSlider");
		    
			wantedResX=PlayerPrefs.GetInt("wantedResolutionX");
			wantedResY=PlayerPrefs.GetInt("wantedResolutionY");
			currentResolutionText.text=wantedResX+"x"+wantedResY;

			int toggle = PlayerPrefs.GetInt("FPSToggle");
			if (toggle==1)
			{
				FPSToggle.isOn=true;
				showFPS=true;
			}
			else 
			{
				FPSToggle.isOn=false;
				showFPS=false;
			}

		    toggle = PlayerPrefs.GetInt("windowedModeToggle");
		    if (toggle==1)
		   	    windowedModeToggle.isOn=true;		
		    else windowedModeToggle.isOn=false;
					    
		    toggle=PlayerPrefs.GetInt("vSyncToggle");
		    if (toggle==1)
		    	vSyncToggle.isOn=true;
		    else vSyncToggle.isOn=false;
		}
		else //no player prefs are saved.
		{
			//if nothing was saved use the full screen resolutions
			wantedResX=Screen.width;
			wantedResY=Screen.height;
		}
	}

	//for testing/Debugging.
	public void DeletePlayerprefs()
	{
		PlayerPrefs.DeleteKey("prefsSaved");
		PlayerPrefs.DeleteKey("FPSToggle");
		PlayerPrefs.DeleteKey("graphicsSlider");
		PlayerPrefs.DeleteKey("antiAliasSlider");
		PlayerPrefs.DeleteKey("anisotropicModeSlider");
		PlayerPrefs.DeleteKey("anisotropicLevelSlider");		
		PlayerPrefs.DeleteKey("wantedResolutionX");
		PlayerPrefs.DeleteKey("wantedResolutionY");		
		PlayerPrefs.DeleteKey("windowedModeToggle");		
		PlayerPrefs.DeleteKey("vSyncToggle"); 
	}


    //assigns the main camera to all canvasis that are not set to "Screen Space-Overlay".
	void OnLevelWasLoaded()  
	{
        canvasCamera=Camera.main;
		menuTransform.gameObject.SetActive(true);
		Canvas[] X=transform.GetComponentsInChildren<Canvas>();
		foreach (Canvas x in X)
		{
			if (x.worldCamera==null)
				x.worldCamera=canvasCamera;
		}
		menuTransform.gameObject.SetActive(false);
		openMenu=false;
		setMenu=false;
	}
}
