using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;

public class LoadLevel : MonoBehaviour 
{
	private FileManager fManager;	
	public Button[] LevelButtons;

	// Use this for initialization
	void Start ()
	{
		fManager = new FileManager ();
		fManager.BuildSaveStructure ();		
		
		for(int x = 0; x < LevelButtons.Length; x++)
		{			
			LevelButtons[x].interactable = false;
			DataManager.globalData.SaveNames[x] = "Save File [" + x.ToString() + "]";
		}
		
		for(int x = 0; x < DataManager.globalData.SavePaths.Length; x++)
		{
			string _path = Application.dataPath + WorldConstants.WORLD_SAVE_DIR + "/Save " + x;

			LevelButtons[x].interactable = true;

			fManager.BuildLevelData(_path, x);
			fManager.BuildPlayerData(_path, x);
						
			DataManager.globalData.SavePaths[x] = _path;
			
			LevelButtons[x].GetComponentInChildren<Text>().text = DataManager.globalData.SaveNames[x];
			LevelButtons[x].GetComponent<LevelButton>().saveDest = DataManager.globalData.SavePaths[x];
		}

	}
	
	public void LoadGame(LevelButton aLevelButton)
	{
		DataManager.globalData.SavePathToLoad = aLevelButton.saveDest;
		Serializer.Save<GlobalData>(Application.dataPath + WorldConstants.GLOBAL_INFO_DIR + WorldConstants.GLOBAL_INFO_FILE, DataManager.globalData);
		Application.LoadLevel (3);
	}
}
