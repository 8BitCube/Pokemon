using UnityEngine;
using System.Collections;

public class Pokemon : PlayerBase
{
	public CharacterController CharacterController;
	public Renderer SpriteRenderer;
		
	private float m_Value;
	
	private void Start()
	{
		//Ignor collisions with other characters
		Physics.IgnoreLayerCollision (this.gameObject.layer, this.gameObject.layer);
		
		if(CharacterController == null)
			CharacterController = GetComponent<CharacterController>();

		PlayerVisuals.IsAnimating = true;
	}
}
