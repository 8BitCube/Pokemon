using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(MapData))]
public class TileMapInspector : Editor {
	
	public override void OnInspectorGUI() {
		//base.OnInspectorGUI();
		DrawDefaultInspector();
		
		if(GUILayout.Button("Regenerate")) {
			MapData tileMap = (MapData)target;
			tileMap.LinkMap();
			tileMap.BuildMap();
		}
	}
}
