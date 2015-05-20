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

	public bool IsFading = false;
	public bool IsSoundFading = false;

	//Setting to true, will use the last saved file loaded.
	public bool useSavedInfo = false;

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
		if(useSavedInfo == false)
			return;
#endif

		LoadData ();
	}

	public void SaveData() 
	{
		DataManager.Save();

		DataManager.globalData.ListOfPokemon.Clear ();
		LocateCharactersInRange (GameManager.Instance.player.transform.position, 20);

		Serializer.Save<GlobalData>(Application.dataPath + WorldConstants.GLOBAL_INFO_DIR + WorldConstants.GLOBAL_INFO_FILE, DataManager.globalData);
		Serializer.Save<LevelData>(DataManager.globalData.SavePathToLoad + WorldConstants.LEVEL_INFO_FILE, DataManager.levelData);
		Serializer.Save<CharacterData>(DataManager.globalData.SavePathToLoad + WorldConstants.PLAYER_INFO_FILE, DataManager.characterData);
	}

	public void LoadData()
	{
		//If you are running straight from the demo scene and 'UseSavedInfo' is true, you may experiance an error,
		//this is due to the folder structure havn't yet to be created.  To fix this, just load the 'Menu' Scene at least once.  This insures a proper
		//folder structure.
		DataManager.globalData = Serializer.Load<GlobalData>(Application.dataPath + WorldConstants.GLOBAL_INFO_DIR + WorldConstants.GLOBAL_INFO_FILE);
		DataManager.levelData = Serializer.Load<LevelData>(DataManager.globalData.SavePathToLoad + WorldConstants.LEVEL_INFO_FILE);
		DataManager.characterData = Serializer.Load<CharacterData>(DataManager.globalData.SavePathToLoad + WorldConstants.PLAYER_INFO_FILE);


		//Load and Instantiate all the Pokemon we have saved in a previous save.
		for(int x = 0; x < DataManager.globalData.ListOfPokemon.Count; x++)
		{
			GameObject _obj = Instantiate(Resources.Load("Prefabs/Characters/Pokemon/Pokemon"), new Vector3(0,0,0), Quaternion.identity) as GameObject;
			_obj.GetComponent<Pokemon>().SetPokemon(DataManager.globalData.ListOfPokemon[x].ID);
			_obj.name = DataManager.globalData.ListOfPokemon[x].CharacterName;

			_obj.transform.position = new Vector3(DataManager.globalData.ListOfPokemon[x].CharacterPosition.x,
			                                      DataManager.globalData.ListOfPokemon[x].CharacterPosition.y,
			                                      DataManager.globalData.ListOfPokemon[x].CharacterPosition.z);
		}
		
		DataManager.Load ();
	}

	private void LocateCharactersInRange(Vector3 center, float radius)
	{
		Collider[] _hitColliders = Physics.OverlapSphere(center, radius);

		Debug.Log (_hitColliders.Length);
		int i = 0;
		while (i < _hitColliders.Length) 
		{
			//If the _hitCollider[i] is a Pokemon 
			if(_hitColliders[i].tag == WorldConstants.POKEMON_TAG)
			{
				PokemonData pData = new PokemonData();
				pData.CharacterName = _hitColliders[i].name;

				pData.ID = _hitColliders[i].GetComponent<Pokemon>().ID;

				pData.CharacterPosition.x = _hitColliders[i].transform.position.x;
				pData.CharacterPosition.y = _hitColliders[i].transform.position.y;
				pData.CharacterPosition.z = _hitColliders[i].transform.position.z;
				
				DataManager.globalData.ListOfPokemon.Add(pData);
			}

			i++;
		}
	}
		
	/// <summary>
	/// Allows the player movement input.
	/// This class will check other statments in the game.  
	/// There can be multiple reasons why the player isn't allowed 
	/// to have movement input;  animations, dialog, fading etc.
	/// </summary>
	/// <returns><c>true</c>, if player movement input was allowed, <c>false</c> otherwise.</returns>
	public bool AllowPlayerMovementInput()
	{
		if((GameManager.Instance.IsFading == false) && true && true && true)
			return true;

		return false;
	}

	/// <summary>
	/// Allows the player action input.
	/// This class will check other statments in the game.  
	/// There can be multiple reasons why the player isn't allowed 
	/// to press the 'Action' Input; fading, well fading is the only one I can think of at this moment.
	/// </summary>
	/// <returns><c>true</c>, if player movement input was allowed, <c>false</c> otherwise.</returns>
	public bool AllowPlayerActionInput()
	{
		if((GameManager.Instance.IsFading == false) && true && true && true)
			return true;
		
		return false;
	}
}