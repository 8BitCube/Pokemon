using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokemonDatabase : MonoBehaviour {

	public static Texture2D[] pokemonSprites;
	public static CharacterStats[] baseStats;
	public static GameObject basis;

	public Texture2D[] sprites;
	public CharacterStats[] stats;
	public GameObject template;

	// Use this for initialization
	void Awake () {
		PokemonDatabase.pokemonSprites=sprites;
		PokemonDatabase.baseStats=stats;
		PokemonDatabase.basis=template;
	}
}
