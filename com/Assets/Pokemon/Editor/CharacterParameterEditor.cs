using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterParameters))]
public class CharacterParameterEditor : Editor
{
	private CharacterParameters myTarget;
	private bool showBooleans = true;
	
	void Awake()
	{
		myTarget=(CharacterParameters)target;
	}
	
	public override void OnInspectorGUI()
	{
		myTarget.Name = EditorGUILayout.TextField ("Name", myTarget.Name);
		myTarget.Speed = EditorGUILayout.IntField ("Speed", myTarget.Speed);

		myTarget.SpriteSheet = Resources.Load("Images/SpriteSheets/" + myTarget.Name) as Texture2D; 

		showBooleans = EditorGUILayout.Foldout (showBooleans, "What can this parameter do?");
		if(showBooleans)
		{			
			myTarget.CanRun = EditorGUILayout.Toggle ("Can we Run?", myTarget.CanRun);
			myTarget.CanBike = EditorGUILayout.Toggle ("Can we Bike?", myTarget.CanBike);
			myTarget.CanJump = EditorGUILayout.Toggle ("Can we Jump?", myTarget.CanJump);
			
			myTarget.CanFish = EditorGUILayout.Toggle ("Can we Fish?", myTarget.CanFish);
			
			myTarget.CanSwim = EditorGUILayout.Toggle ("Can we Swim?", myTarget.CanSwim);
			myTarget.CanDive = EditorGUILayout.Toggle ("Can we Dive?", myTarget.CanDive);	
			
			myTarget.CanFly = EditorGUILayout.Toggle ("Can we Fly?", myTarget.CanFly);
			
			myTarget.CanForward = EditorGUILayout.Toggle ("Can we move fowards?", myTarget.CanForward);
			myTarget.CanReverse = EditorGUILayout.Toggle ("Can we move backwards?", myTarget.CanReverse);
			myTarget.CanRotate = EditorGUILayout.Toggle ("Can we Rotate?", myTarget.CanRotate);
			myTarget.CanStraf = EditorGUILayout.Toggle ("Can we Straf?", myTarget.CanStraf);

		}

		if ( GUI.changed )
		{
			EditorUtility.SetDirty(target);
		}
	}
}