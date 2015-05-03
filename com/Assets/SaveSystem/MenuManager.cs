using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour 
{
	public Menu CurrentMenu;

	public Button[] LevelButtons;
	public LevelButton LevelButton;
	public GameObject LevelButtonParent;

	public void Start()
	{
		for(int x = 0; x < LevelButtons.Length; x++)
		{			
			LevelButtons[x].GetComponentInChildren<Text>().text = "New Game";
		}

		for(int x = 0; x < DataManager.gameData.Saves.Length; x++)
		{
			LevelButtons[x].GetComponentInChildren<Text>().text = DataManager.gameData.Saves[x];
			LevelButtons[x].GetComponent<LevelButton>().saveDest = DataManager.gameData.Saves[x];
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
		DataManager.gameData.selectedSave = aLevelButton.saveDest;
		Serializer.Save<GameData>(Application.dataPath + WorldConstants.GLOBAL_INFO_FILE, DataManager.gameData);
		Application.LoadLevel ("Demo");
	}

	public void NewGame()
	{
		Application.LoadLevel ("Demo");
	}
}
