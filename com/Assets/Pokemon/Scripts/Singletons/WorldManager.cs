using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour 
{
	public static WorldManager Instance = null;
	
	public GameObject Player;
	public RPGText uiText;
	
	public bool AllowPhysics = true;
	public bool AllowInput = true;

	void Awake()
	{
		if(Instance == null)
			Instance = this;
	}
}
