using UnityEngine;
using System.Collections;

[System.Serializable]
public class GlobalData 
{
	public string[] Saves = new string[4];
	public string[] SaveNames = new string[4];

	public string selectedSave;

	public float MusicVolume;
	public float SFXVolume;
}
