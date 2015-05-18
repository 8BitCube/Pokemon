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
		#if UNITY_EDITOR
		//Disable the function to press esc if we dont use the save feature.
		if(GameManager.Instance.useSavedInfo == false)
			return;

		#endif
		//Temporary, TODO:: Add to the PlayerInput.cs HandleActionInput() function.
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SoundManager.Instance.PlaySFX(selectSound);
			MenuManager.Instance.ShowMenu((MenuManager.Instance.CurrentMenu == pauseMenu) ? noMenu : pauseMenu);
		}
	}

	/// <summary>
	/// Activates a sound and calls the save game method in GameManager.
	/// </summary>
	public void SaveViaButton()
	{
		SoundManager.Instance.PlaySFX(selectSound);
		MenuManager.Instance.CurrentMenu = noMenu;

		GameManager.Instance.SaveData ();
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
