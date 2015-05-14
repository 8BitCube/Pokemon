using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.10.2015
/// Definition:  The Menu Manager will handle which menu will be shown.
/// </summary>
public class MenuManager : MonoBehaviour 
{
	public static MenuManager Instance;
	public Menu CurrentMenu;

	public void Start()
	{
		if(Instance == null)
			Instance = this;

		if(CurrentMenu != null)
			ShowMenu (CurrentMenu);
	}

	public void ShowMenu(Menu menu)
	{
		if (CurrentMenu != null)
			CurrentMenu.IsOpen = false;

		CurrentMenu = menu;
		CurrentMenu.IsOpen = true;
	}
}
