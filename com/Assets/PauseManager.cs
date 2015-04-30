using UnityEngine; 
using System.Collections; 

public class PauseManager : MonoBehaviour 
{ 
	public GameObject PauseMenuObject = null;

	// define and create our GUI delegate
	private delegate void GUIMethod(); 
	private GUIMethod currentGUIMethod; 
	
	void Start () 
	{ 
		PauseMenuObject.SetActive (false);
	} 
	
	public void MainMenu() 
	{ 

	} 
	
	private void PauseMenu()
	{

	}    
	
	// Update is called once per frame 
	public void Update () 
	{ 
		if(Input.GetKeyDown(KeyCode.Escape))
			PauseMenuObject.SetActive (!PauseMenuObject.activeSelf);
	} 
}