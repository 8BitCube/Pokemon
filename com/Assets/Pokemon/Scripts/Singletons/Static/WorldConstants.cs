using UnityEngine;
using System.Collections;

public static class WorldConstants 
{
	public const string PLAYER_TAG = "Player";
	public const string POKEMON_TAG = "Pokemon";
	public const string NPC_TAG = "NPC";
	
	public const string GAME_NAME = "Pokemon";
	public const string COMPANY_NAME = "8 Bit Cube";

	#region Directories
	public const string WORLD_SAVE_DIR = "/WorldSaves";
	public const string SOUND_AUDIO_DIR = "Sounds/AudioTypes/";
	public const string GLOBAL_INFO_DIR = "/WorldSaves/GlobalData";
	public const string POKEMON_SPRITE_DIR = "Images/SpriteSheets/Pokemon/";
	#endregion

	#region Files
	public const string GLOBAL_INFO_FILE = "/GlobalData.dat";
	public const string PLAYER_INFO_FILE = "/Player.dat";
	public const string LEVEL_INFO_FILE = "/Level.dat";
	#endregion

	#region New Game Values
	public const float DEFAULT_POS_X = 16.522f;
	public const float DEFAULT_POS_Y = 0.9f;
	public const float DEFAULT_POS_Z = -26.74f;

	public const float DEFAULT_MASTER_V = 0.5f; //TODO:: Impletement feature, no use so far.
	public const float DEFAULT_MUSIC_V = 0.5f;
	public const float DEFAULT_SFX_V = 0.5f;

	public const int   DEFAULT_MUSICID = 0;
	#endregion
}
