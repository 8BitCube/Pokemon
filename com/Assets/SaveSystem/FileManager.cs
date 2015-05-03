using UnityEngine;
using System.Collections;

public class FileManager : MonoBehaviour 
{
	public static FileManager Instance;
	// Use this for initialization
	void Awake () 
	{
		if(Instance == null)
			Instance = this;

		//File our folder structor
		CheckDirectory (Application.dataPath + WorldConstants.WORLD_SAVE_DIR);
		CheckDirectory (Application.dataPath + WorldConstants.WORLD_SAVE_1_DIR);
		CheckDirectory (Application.dataPath + WorldConstants.WORLD_SAVE_2_DIR);
		CheckDirectory (Application.dataPath + WorldConstants.WORLD_SAVE_3_DIR);
		CheckDirectory (Application.dataPath + WorldConstants.WORLD_SAVE_4_DIR);

		if(CheckFile(Application.dataPath + WorldConstants.GLOBAL_INFO_FILE))
			DataManager.gameData = Serializer.Load<GameData>(Application.dataPath + WorldConstants.GLOBAL_INFO_FILE);
		else
			DataManager.gameData = new GameData ();

		DataManager.gameData.Saves = System.IO.Directory.GetFiles(Application.dataPath + WorldConstants.WORLD_SAVE_DIR, "*.dat");
	}

	private void CheckDirectory(string directoryPath)
	{
		//check if directory doesn't exit
		if(!System.IO.Directory.Exists(directoryPath))
			System.IO.Directory.CreateDirectory(directoryPath);
	}

	private bool CheckFile(string directoryPath)
	{
		return (System.IO.File.Exists (directoryPath));
	}
}
