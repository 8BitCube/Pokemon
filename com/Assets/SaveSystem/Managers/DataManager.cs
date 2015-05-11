using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Andrew Mills
/// Date Modified: 5.10.2015
/// Definition:  
/// </summary>
public class DataManager 
{
	public delegate void DataEventHandler ();

	public static event DataEventHandler onLoad;
	public static event DataEventHandler onSave;

	public static GlobalData globalData;
	public static PlayerData playerData;

	/// <summary>
	/// Holds a static load feature that will load all subscribed instances
	/// </summary>
	public static void Load ()
	{
		if (onLoad != null)
			onLoad ();
	}

	/// <summary>
	/// Holds a static save feature that will save all subscribed instances
	/// </summary>
	public static void Save ()
	{
		if (onSave != null)
			onSave ();
	}
}