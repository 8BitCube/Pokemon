using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.14.2015
/// Definition:  File Manager will build the save structure in case we do not have it.  This class should only need to be built once.
/// </summary>
public class FileManager 
{
	/// <summary>
	/// Builds the save structure.
	/// </summary>
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

	/// <summary>
	/// Loads the GlobalData or creates a new one.
	/// </summary>
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

	/// <summary>
	/// Loads player data based on the saveValue passed in, 
	/// </summary>
	/// <param name="aSaveValue">A save value.</param>
	public void BuildPlayerData(int aSaveValue)
	{
		string _path = Application.dataPath + WorldConstants.WORLD_SAVE_DIR + "/Save " + aSaveValue;

		if (File.Exists (_path + WorldConstants.PLAYER_INFO_FILE))
			DataManager.playerData = Serializer.Load<PlayerData>(_path + WorldConstants.PLAYER_INFO_FILE);
		else
		{
			DataManager.playerData = new PlayerData ();
			DataManager.playerData.PlayerPos.x = WorldConstants.DEFAULT_POS_X;
			DataManager.playerData.PlayerPos.y = WorldConstants.DEFAULT_POS_Y;
			DataManager.playerData.PlayerPos.z = WorldConstants.DEFAULT_POS_Z;
			
			Serializer.Save<PlayerData>(_path + WorldConstants.PLAYER_INFO_FILE, DataManager.playerData);
		}

		DataManager.globalData.SavePaths[aSaveValue] = _path + WorldConstants.PLAYER_INFO_FILE;
	}

	/// <summary>
	/// Deletes the directory passed in.
	/// </summary>
	/// <param name="sourcePath">Source path.</param>
	private void DeleteDirectory(string sourcePath)
	{

	}

	/// <summary>
	/// Copies the directory.
	/// </summary>
	/// <param name="sourcePath">Source path.</param>
	/// <param name="destPath">Destination path.</param>
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

	/// <summary>
	/// Builds the directory. But it makes a check first
	/// </summary>
	/// <param name="directoryPath">Directory path.</param>
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
