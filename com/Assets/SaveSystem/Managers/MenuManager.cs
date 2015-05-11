using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.10.2015
/// Definition:  
/// </summary>
public class MenuManager : MonoBehaviour 
{
	public Menu CurrentMenu;
	public Button[] LevelButtons;

	public void Start()
	{
		for(int x = 0; x < LevelButtons.Length; x++)
		{			
			LevelButtons[x].interactable = false;
			DataManager.globalData.SaveNames[x] = "Save File [" + x.ToString() + "]";
		}

		for(int x = 0; x < DataManager.globalData.Saves.Length; x++)
		{
			LevelButtons[x].interactable = true;
			LevelButtons[x].GetComponentInChildren<Text>().text = DataManager.globalData.SaveNames[x];
			LevelButtons[x].GetComponent<LevelButton>().saveDest = DataManager.globalData.Saves[x];
		}

		ShowMenu (CurrentMenu);
	}

	public void ShowMenu(Menu menu)
	{
		if (CurrentMenu != null)
			CurrentMenu.IsOpen = false;

		CurrentMenu = menu;
		CurrentMenu.IsOpen = true;
	}

	public void LoadGame(LevelButton aLevelButton)
	{
		DataManager.globalData.selectedSave = aLevelButton.saveDest;
		Serializer.Save<GlobalData>(Application.dataPath + WorldConstants.GLOBAL_INFO_FILE, DataManager.globalData);
		Application.LoadLevel (3);
	}
}
