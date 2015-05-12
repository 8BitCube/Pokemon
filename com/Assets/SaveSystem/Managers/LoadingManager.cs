using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.10.2015
/// Definition:  This script will load the next scene in an async fashion, this allow the program to animate a loading screen if needed.
/// </summary>
public class LoadingManager : MonoBehaviour
{
	public string levelToLoad;
	private int loadProgress = 0;

	void Start () 
	{
		//Begin our coroutine as soon as this scene begins.
		StartCoroutine (DisplayLoadingScreen (levelToLoad));
	}

	/// <summary>
	/// Displaies the loading screen.
	/// </summary>
	/// <returns>The loading screen.</returns>
	/// <param name="level">Level.</param>
	public IEnumerator DisplayLoadingScreen(string level)
	{
		AsyncOperation async = Application.LoadLevelAsync (level);

		//Loop until the loading is complete.
		while(!async.isDone)
		{
			loadProgress = (int)(async.progress * 100);
			yield return null;
		}

		//Technically this will never be reached.  For good practise and possible future features, it has been placed.
		yield return null;
	}
}
