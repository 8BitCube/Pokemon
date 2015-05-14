using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.13.2015
/// Definition:  PauseManager will hold functions that buttons as of unity 4.6+ can access via the button "On Click" event.
/// </summary>
public class PauseManager : MonoBehaviour 
{	
	public Menu pauseMenu;
	public Menu noMenu;
	public AudioClip selectSound;
	
	void Update ()
	{
		//Temporary, TODO:: Add to the PlayerInput.cs HandleActionInput() function.
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SoundManager.Instance.PlaySFX(selectSound);
			MenuManager.Instance.ShowMenu((MenuManager.Instance.CurrentMenu == pauseMenu) ? noMenu : pauseMenu);
		}
	}

	/// <summary>
	/// Saves the game.
	/// </summary>
	public void SaveViaButton()
	{
		DataManager.Save();
		SoundManager.Instance.PlaySFX(selectSound);
		
		Serializer.Save<GlobalData>(Application.dataPath + WorldConstants.GLOBAL_INFO_DIR + WorldConstants.GLOBAL_INFO_FILE, DataManager.globalData);
		Serializer.Save<PlayerData>(DataManager.globalData.selectedSave, DataManager.playerData);

		MenuManager.Instance.CurrentMenu = null;
	}

	//LoadGame currently does nothing fun
	public void LoadViaButton()
	{
		SoundManager.Instance.PlaySFX(selectSound);
	}
	
	/// <summary>
	/// Loads the menu.
	/// </summary>
	public void ExitViaButton()
	{
		Application.LoadLevel ("Menu");
	}
}
