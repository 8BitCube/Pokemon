using UnityEngine;
using System.Collections;

public class Pokemon : PlayerBase
{
	public CharacterController CharacterController;
	
	private float m_Value;

	public CharacterStats myStats;

	GameObject from;
	
	private void Start(){
		//Ignor collisions with other characters
		Physics.IgnoreLayerCollision (this.gameObject.layer, this.gameObject.layer);
		
		if(CharacterController == null)
			CharacterController = GetComponent<CharacterController>();
		
		PlayerVisuals.IsAnimating = true;
	}

	public void SetPokemon(int id){

		PlayerVisuals.defaultTexture=PokemonDatabase.pokemonSprites[id];
		PlayerVisuals.myRenderer.material.mainTexture=PlayerVisuals.defaultTexture;

		PlayerVisuals.UpdateImage();

	}
}