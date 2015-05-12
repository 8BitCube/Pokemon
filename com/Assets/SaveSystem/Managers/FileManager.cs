using UnityEngine;
using System.Collections;
using System.IO;

public class FileManager 
{
	public void BuildSaveStructure()
	{		
		BuildDirectory (Application.dataPath + WorldConstants.WORLD_SAVE_DIR);
		BuildDirectory (Application.dataPath + WorldConstants.GLOBAL_INFO_DIR);

		BuildGlobalData ();

		for(int x = 0; x < DataManager.globalData.SavePaths.Length; x++)
		{
			string _path = Application.dataPath + WorldConstants.WORLD_SAVE_DIR + "/Save " + x;
			BuildDirectory (_path);

			DataManager.globalData.SavePaths[x] = _path + WorldConstants.PLAYER_INFO_FILE;
			DataManager.globalData.SaveNames[x] = x.ToString();
		}

		//For Testing builds.  Save one will always be the default build.
		DataManager.globalData.selectedSave = DataManager.globalData.SavePaths [0];
	}

	public void BuildGlobalData()
	{
		string _path = Application.dataPath + WorldConstants.GLOBAL_INFO_DIR + WorldConstants.GLOBAL_INFO_FILE;

		if (CheckFileExsist (_path)) 
			DataManager.globalData = Serializer.Load<GlobalData>(_path);
		else
		{
			DataManager.globalData = new GlobalData ();
			DataManager.globalData.SavePaths = new string[4];
			DataManager.globalData.SaveNames = new string[4];
			DataManager.globalData.MusicVolume = WorldConstants.DEFAULT_MUSIC_V;
			DataManager.globalData.SFXVolume = WorldConstants.DEFAULT_SFX_V;

			Serializer.Save<GlobalData>(_path, DataManager.globalData);
		}
	}

	public void BuildPlayerData(int aSaveValue)
	{
		string _path = Application.dataPath + WorldConstants.WORLD_SAVE_DIR + "/Save " + aSaveValue;

		if (File.Exists (_path + WorldConstants.PLAYER_INFO_FILE))
			DataManager.playerData = Serializer.Load<PlayerData>(_path + WorldConstants.PLAYER_INFO_FILE);
		else
		{
			Debug.Log("Built a new Player Data file");
			DataManager.playerData = new PlayerData ();
			DataManager.playerData.PlayerPos.x = WorldConstants.DEFAULT_POS_X;
			DataManager.playerData.PlayerPos.y = WorldConstants.DEFAULT_POS_Y;
			DataManager.playerData.PlayerPos.z = WorldConstants.DEFAULT_POS_Z;
			
			Serializer.Save<PlayerData>(_path + WorldConstants.PLAYER_INFO_FILE, DataManager.playerData);
		}

		DataManager.globalData.SavePaths[aSaveValue] = _path + WorldConstants.PLAYER_INFO_FILE;
	}

	private void DeleteDirectory(string sourcePath)
	{

	}

	private static void CopyDirectory(string sourcePath, string destPath)
	{
		if (!Directory.Exists(destPath))
			Directory.CreateDirectory(destPath);
		
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

	private void BuildDirectory(string directoryPath)
	{
		//check if directory doesn't exit
		if(!Directory.Exists(directoryPath))
		{
			Debug.LogWarning("Path < " + directoryPath + " > :: Does not exsist, creating one.");
			Directory.CreateDirectory(directoryPath);
		}
	}

	private bool CheckFileExsist(string directoryPath)
	{
		return (File.Exists (directoryPath));
	}
}
