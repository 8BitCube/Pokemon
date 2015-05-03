using UnityEngine;
using System.IO;

/// Author: Andrew Mills
/// Last Edit: 5.2.2015
/// Definition: This GameManager is a universal script, only one should ever exsist
public class GameManager : MonoBehaviour
{
	public static GameManager Instance = null;
	public Player Player;
	public GameObject PauseMenu;

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
		DataManager.gameData = Serializer.Load<GameData>(Application.dataPath + WorldConstants.GLOBAL_INFO_FILE);
		DataManager.playerData = Serializer.Load<PlayerData>(DataManager.gameData.selectedSave);

		if(UseSavedInfo)
			DataManager.Load ();

		if(DataManager.gameData == null)
			DataManager.gameData = new GameData ();

		if(DataManager.playerData == null)
			DataManager.playerData = new PlayerData ();
	}

	public void SaveGame()
	{
		DataManager.Save();
		Serializer.Save<PlayerData>(DataManager.gameData.selectedSave, DataManager.playerData);
	}

	//Update is called every frame.
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
			PauseMenu.SetActive(!PauseMenu.activeSelf);

		if (Input.GetKeyDown (KeyCode.P) && UseSavedInfo)
			SaveGame ();
	}
}