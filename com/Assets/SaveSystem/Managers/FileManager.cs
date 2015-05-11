using UnityEngine;
using System.Collections;
using System.IO;

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
		CheckDirectory (Application.dataPath + WorldConstants.WORLD_SAVE_DIR + "/Backup");
		CheckDirectory (Application.dataPath + WorldConstants.WORLD_SAVE_DIR + "/GlobalInformation");
		CheckDirectory (Application.dataPath + WorldConstants.WORLD_SAVE_1_DIR);
		CheckDirectory (Application.dataPath + WorldConstants.WORLD_SAVE_2_DIR);
		CheckDirectory (Application.dataPath + WorldConstants.WORLD_SAVE_3_DIR);
		CheckDirectory (Application.dataPath + WorldConstants.WORLD_SAVE_4_DIR);

		if(CheckFileExsist(Application.dataPath + WorldConstants.GLOBAL_INFO_FILE))
			DataManager.globalData = Serializer.Load<GlobalData>(Application.dataPath + WorldConstants.GLOBAL_INFO_FILE);
		else
			BuildGlobalData();

		for(int x = 0; x < 4; x++)
		{
			DataManager.globalData.Saves [x] = Application.dataPath + WorldConstants.WORLD_SAVE_DIR + "Save " + (x+1) + "/Player.dat";
		}
	}

	private void BuildGlobalData()
	{
		DataManager.globalData = new GlobalData ();
		Serializer.Save<GlobalData>(Application.dataPath + WorldConstants.GLOBAL_INFO_FILE, DataManager.globalData);

		// This Chucnk of code will build a "New Game" Based on if the files were found.
		for(int x = 0; x < 4; x++)
		{
			string _path = Application.dataPath + WorldConstants.WORLD_SAVE_DIR + "Save " + (x+1) + "/Player.dat";

			if(!CheckFileExsist(_path))
			{
				DataManager.playerData = new PlayerData();
				Serializer.Save<PlayerData>(_path, DataManager.playerData);
				DataManager.globalData.SaveNames [x] = "New Game";
			}
		}
	}

	private void DeleteDirectory(string sourcePath)
	{

	}

	private static void CopyDirectory(string sourcePath, string destPath)
	{
		if (!Directory.Exists(destPath))
		{
			Directory.CreateDirectory(destPath);
		}
		
		foreach (string file in Directory.GetFiles(sourcePath))
		{
			string dest = Path.Combine(destPath, Path.GetFileName(file));
			File.Copy(file, dest);
		}
		
		foreach (string folder in Directory.GetDirectories(sourcePath))
		{
			string dest = Path.Combine(destPath, Path.GetFileName(folder));
			CopyDirectory(folder, dest);
		}
	}

	private void CheckDirectory(string directoryPath)
	{
		//check if directory doesn't exit
		if(!Directory.Exists(directoryPath))
			Directory.CreateDirectory(directoryPath);
	}

	private bool CheckFileExsist(string directoryPath)
	{
		return (File.Exists (directoryPath));
	}
}
