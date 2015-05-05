using UnityEngine;
using System.Collections;

public class SpriteMeshRenderer : MonoBehaviour {

	SpriteMesh spr;
	public Texture2D texture;

	public float scale;

	// Use this for initialization
	void Start () {
		spr = new SpriteMesh(texture);
		GenerateSpriteMesh();
	}

	void GenerateSpriteMesh(){
		spr.scale=scale/10;
		spr.depth=0.04f;
		GetComponent<MeshFilter>().mesh=spr.depthMesh(new Vector2(0,0), new Vector2(texture.width,texture.height));
		GetComponent<MeshRenderer>().material.mainTexture=texture;
	}


}
