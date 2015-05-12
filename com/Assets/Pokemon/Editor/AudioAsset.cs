using UnityEngine;
using UnityEditor;
using System;

public class AudioAsest
{
	[MenuItem("Assets/Create/AudioAsset")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<AudioInformation>();
	}
}