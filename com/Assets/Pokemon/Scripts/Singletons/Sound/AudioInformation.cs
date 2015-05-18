using UnityEngine;
using System.Collections;

public class AudioInformation : ScriptableObject
{
	public string Name;
	public int ID;
	public AudioClip clip;
	
	public int Channels;
	public bool IsLooped;
	public int SampleRate;
	public int LoopStartSample;
	public int NumSamples;
	public int DataOffset;
	public int NumBlocks;
	public int BlockSize;
	
	public int BitPerSample;
}
