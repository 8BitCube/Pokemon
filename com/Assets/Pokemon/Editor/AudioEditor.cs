using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioInformation))]
public class DialogueEditor : Editor
{
	private AudioInformation myTarget; //Make an easy shortcut to the AudioInformation your editing
	void Awake()
	{
		myTarget=(AudioInformation)target;
	}
	
	public override void OnInspectorGUI()
	{
		myTarget.Name = EditorGUILayout.TextField ("Name", myTarget.Name);
		myTarget.clip = Resources.Load("Sounds/Music/" + myTarget.Name) as AudioClip; 
		myTarget.IsLooped = EditorGUILayout.Toggle("IsLooped", myTarget.IsLooped);
		myTarget.SampleRate = EditorGUILayout.IntField("SampleRate", myTarget.SampleRate);
		myTarget.LoopStartSample = EditorGUILayout.IntField("LoopStartSample", myTarget.LoopStartSample);
		myTarget.NumSamples = EditorGUILayout.IntField("NumSamples", myTarget.NumSamples);
		myTarget.DataOffset = EditorGUILayout.IntField("DataOffset", myTarget.DataOffset);
		myTarget.NumBlocks = EditorGUILayout.IntField("NumBlocks", myTarget.NumBlocks);
		myTarget.BlockSize = EditorGUILayout.IntField("BlockSize", myTarget.BlockSize);
		myTarget.BitPerSample = EditorGUILayout.IntField("BitPerSample", myTarget.BitPerSample);
	}
}