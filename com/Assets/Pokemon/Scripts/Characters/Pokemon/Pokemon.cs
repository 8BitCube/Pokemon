using UnityEngine;
using System.Collections;

public class Pokemon : CharacterBase
{
	public CharacterParameters CurrentParameters { get; set; }
	
	public CharacterParameters WalkParameter;
	public CharacterParameters RunParameter;

	public int ID;

	public CharacterStats myStats;

	GameObject from;
	
	public void Awake()
	{
		//Ignor collisions with other characters
		Physics.IgnoreLayerCollision (this.gameObject.layer, this.gameObject.layer);
		
		CurrentParameters = WalkParameter;
		
		DataManager.onLoad += this.Load;
		DataManager.onSave += this.Save;
	}
	
	public void OnDestroy()
	{
		DataManager.onLoad -= this.Load;
		DataManager.onSave -= this.Save;
	}
	
	public void Load()
	{
		this.transform.position = new Vector3( 
		                                      DataManager.characterData.CharacterPosition.x, 
		                                      DataManager.characterData.CharacterPosition.y, 
		                                      DataManager.characterData.CharacterPosition.z);
	}
	
	public void Save()
	{
		DataManager.characterData.CharacterPosition.x = this.transform.position.x;
		DataManager.characterData.CharacterPosition.y = this.transform.position.y;
		DataManager.characterData.CharacterPosition.z = this.transform.position.z;
	}

	public void Update()
	{
		if (Movement.MoveVector.x != 0 || Movement.MoveVector.z != 0) 
			CharacterVisuals.IsAnimating = true;
		else
			if(CharacterVisuals.IsAnimating != false)
		{
			CharacterVisuals.IsAnimating = false;
			CharacterVisuals.UpdateIdolImage();
		}
	}

	public void SetPokemon(int aID)
	{
		//Go to the image location
		CharacterVisuals.defaultTexture = Resources.Load(WorldConstants.POKEMON_SPRITE_DIR + aID.ToString("000") + "_0") as Texture2D;
	}
}