using UnityEngine;
using System.Collections;

public class CharacterBase : MonoBehaviour 
{
	Motor m_Movement;
	public Motor Movement
	{
		get
		{
			if(m_Movement == null)
				m_Movement = GetComponent<Motor>();
			
			return m_Movement;
		}
	}

	CharacterVisuals m_CharacterVisuals;
	public CharacterVisuals CharacterVisuals
	{
		get
		{
			if(m_CharacterVisuals == null)
				m_CharacterVisuals = GetComponent<CharacterVisuals>();
			
			return m_CharacterVisuals;
		}
	}

}
