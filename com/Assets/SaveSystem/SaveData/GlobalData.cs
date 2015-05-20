using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GlobalData 
{
	public string[] SavePaths;
	public string[] SaveNames;

	public string SavePathToLoad;

	public float MusicVolume;
	public float SFXVolume;

	public List<PokemonData> ListOfPokemon;

}
