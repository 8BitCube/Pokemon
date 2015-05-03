using UnityEngine;
using System.Collections;

public class DataManager 
{
	public delegate void DataEventHandler ();

	public static event DataEventHandler onLoad;
	public static event DataEventHandler onSave;

	public static GameData gameData;
	public static PlayerData playerData;

	public static string destinationValue;

	public static void Load ()
	{
		if (onLoad != null)
			onLoad ();
	}

	public static void Save ()
	{
		if (onSave != null)
			onSave ();
	}
}
