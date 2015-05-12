﻿using UnityEngine;
using System.Collections;

public static class WorldConstants 
{
	public const string PLAYER_TAG = "Player";
	public const string COMPANY_NAME = "8 Bit Cube";
	public const string GAME_NAME = "Pokemon";

	#region Directories
	public const string WORLD_SAVE_DIR = "/WorldSaves";
	public const string GLOBAL_INFO_DIR = "/WorldSaves/GlobalData";
	#endregion

	#region Files
	public const string GLOBAL_INFO_FILE = "/GlobalData.dat";
	public const string PLAYER_INFO_FILE = "/Player.dat";
	#endregion

	#region New Game Values
	public const float DEFAULT_POS_X = 16.522f;
	public const float DEFAULT_POS_Y = 0.9f;
	public const float DEFAULT_POS_Z = -26.74f;

	public const float DEFAULT_MUSIC_V = 0.5f;
	public const float DEFAULT_SFX_V = 0.5f;
	#endregion
}
