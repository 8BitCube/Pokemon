using UnityEngine;
using UnityEditor;
using System;

public class CharacterParameterAsset
{
	[MenuItem("Assets/Create/CharacterParameterAsset")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CharacterParameters>();
	}
}