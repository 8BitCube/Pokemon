using UnityEngine;
using System.Collections;

public class BaseController : MonoBehaviour 
{
	Player m_Player;
	public Player Player
	{
		get
		{
			if(m_Player == null)
				m_Player = GetComponent<Player>();
			
			return m_Player;
		}
	}
	
	PlayerInput m_PlayerInput;
	public PlayerInput PlayerInput
	{
		get
		{
			if(m_PlayerInput == null)
				m_PlayerInput = GetComponent<PlayerInput>();
			
			return m_PlayerInput;
		}
	}
	
	Motor m_Movement;
	public Motor PlayerMovement
	{
		get
		{
			if(m_Movement == null)
				m_Movement = GetComponent<Motor>();
			
			return m_Movement;
		}
	}
	
	PlayerVisuals m_PlayerVisuals;
	public PlayerVisuals PlayerVisuals
	{
		get
		{
			if(m_PlayerVisuals == null)
				m_PlayerVisuals = GetComponent<PlayerVisuals>();
			
			return m_PlayerVisuals;
		}
	}
}
