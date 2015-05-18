using UnityEngine;
using System.Collections;

public class CharacterBase : MonoBehaviour 
{
	Character m_Character;
	public Character Character
	{
		get
		{
			if(m_Character == null)
				m_Character = GetComponent<Character>();

			return m_Character;
		}
	}

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

	CharacterVisuals m_PlayerVisuals;
	public CharacterVisuals PlayerVisuals
	{
		get
		{
			if(m_PlayerVisuals == null)
				m_PlayerVisuals = GetComponent<CharacterVisuals>();
			
			return m_PlayerVisuals;
		}
	}

}
