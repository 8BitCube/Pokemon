using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameData 
{
	public string[] Saves;
	public string selectedSave;

	public float MusicVolume { get; set; }
	public float SFXVolume { get; set; }
}
