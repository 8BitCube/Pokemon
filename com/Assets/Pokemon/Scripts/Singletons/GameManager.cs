using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.10.2015
/// Definition:  
/// </summary>
public class GameManager : MonoBehaviour
{
	public static GameManager Instance = null;

	public GameObject Player;
	public GameObject PauseMenu;
	public AudioClip SelectSound;

	//Setting to true, will use the last saved file loaded.
	public bool UseSavedInfo = false;

	//Awake is always called before any Start functions
	void Start()
	{
		//Check if instance already exists
		if (Instance == null)
			Instance = this;

		//Call the InitGame function to initialize the first level 
		InitGame();
	}

	//Initializes the game for each level.
	void InitGame()
	{
		StartCoroutine (YieldTransition ());

		if(UseSavedInfo == false)
			return;

		DataManager.globalData = Serializer.Load<GlobalData>(Application.dataPath + WorldConstants.GLOBAL_INFO_FILE);
		DataManager.playerData = Serializer.Load<PlayerData>(DataManager.globalData.selectedSave);

		DataManager.Load ();
		
		if(DataManager.globalData == null)
			DataManager.globalData = new GlobalData ();
		
		if(DataManager.playerData == null)
			DataManager.playerData = new PlayerData ();
	}

	/// <summary>
	/// Saves the game.
	/// </summary>
	public void SaveGame()
	{
		DataManager.Save();
		SoundManager.Instance.PlaySFX(SelectSound);

		Serializer.Save<GlobalData>(Application.dataPath + WorldConstants.GLOBAL_INFO_FILE, DataManager.globalData);
		Serializer.Save<PlayerData>(DataManager.globalData.selectedSave, DataManager.playerData);
		PauseMenu.SetActive(!PauseMenu.activeSelf);
	}

	//LoadGame currently doesn't work
	public void LoadGame()
	{
		SoundManager.Instance.PlaySFX(SelectSound);
		PauseMenu.SetActive(!PauseMenu.activeSelf);
	}

	/// <summary>
	/// Loads the menu.
	/// </summary>
	public void LoadMenu()
	{
		SoundManager.Instance.PlaySFX(SelectSound);
		Application.LoadLevel ("Menu");
		PauseMenu.SetActive(!PauseMenu.activeSelf);
	}

	//Update is called every frame.
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape) && UseSavedInfo)
		{
			SoundManager.Instance.PlaySFX(SelectSound);
			PauseMenu.SetActive(!PauseMenu.activeSelf);
		}

		if (Input.GetKeyDown (KeyCode.P) && UseSavedInfo)
			SaveGame ();
	}

	/// <summary>
	/// Opens the scene with a smooth fade in transition
	/// </summary>
	/// <returns>The transition.</returns>
	public IEnumerator YieldTransition()
	{
		IEnumerator fadeInEffect = FadeManager.Instance.FadeScreen(FadeManager.FadeType.FadeIn);
		IEnumerator fadeInMusic = FadeManager.Instance.FadeMusic(FadeManager.FadeType.FadeIn);

		while (fadeInEffect.MoveNext())
		{ yield return null; }
		
		yield return null;
	}
}