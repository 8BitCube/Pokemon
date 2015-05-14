using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.10.2015
/// Definition:  The GameManager will handle initial start up, delgating the save and load functions. 
/// 			Use "GameManager.Instance.<value>" to gain access to this class respecfully.
/// 			NOTE:  UseSavedInfo will be removed on release, this is mearly a testing feature.
/// </summary>
public class GameManager : MonoBehaviour
{
	public static GameManager Instance = null;

	//Hold a universal reference to the players gameObjects.  
	public GameObject player;

	//Setting to true, will use the last saved file loaded.
	[SerializeField] private bool m_UseSavedInfo = false;

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
#if UNITY_EDITOR
		if(m_UseSavedInfo == false)
			return;
#endif
		//If you are running straight from the demo scene and 'UseSavedInfo' is true, you may experiance an error,
		//this is due to the folder structure havn't yet to be created.  To fix this, just load the 'Menu' Scene at least once.  This insures a proper
		//folder structure.
		DataManager.globalData = Serializer.Load<GlobalData>(Application.dataPath + WorldConstants.GLOBAL_INFO_DIR + WorldConstants.GLOBAL_INFO_FILE);
		DataManager.playerData = Serializer.Load<PlayerData>(DataManager.globalData.selectedSave);

		DataManager.Load ();
	}
}