using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteMesh {
	
	public List<Vector3> verts = new List<Vector3>();

	public List<int> tris = new List<int>();

	public List<Vector2> UV = new List<Vector2>();

	public Mesh mainMesh = new Mesh();
	Texture2D texture;

	int squareCount;

	public SpriteMesh() { }
	public SpriteMesh(Texture2D sprite){
		texture=sprite;
	}

	public Mesh depthMesh(Vector2 text, Vector2 size){

		Mesh tmp = new Mesh();

		for(int x = 0; x < Mathf.RoundToInt(size.x); x ++){
			for(int y = 0; y < Mathf.RoundToInt(size.y); y ++){

				if(texture.GetPixel((int)text.x+x,(int)text.y+y).a!=0){
					GenSquare(x-(int)(size.x/2),y-(int)(size.y/2), text + new Vector2(x,y));
				}

			}
		}

		UpdateMesh (tmp);

		return tmp;
	}
	public Mesh mesh(Vector2 text, Vector2 size){

		Mesh tmp = new Mesh();
		
		for(int x = 0; x < Mathf.RoundToInt(size.x); x ++){
			for(int y = 0; y < Mathf.RoundToInt(size.y); y ++){
				
				if(texture.GetPixel((int)text.x+x,(int)text.y+y).a!=0){
					GenSquare(x-(int)(size.x/2),y-(int)(size.y/2), text + new Vector2(x,y));
				}
				
			}
		}
		
		UpdateMesh (tmp);
		
		return tmp;
	}

	float scale=0.015f;
	float pixelSize = 0.00390625f;

	void GenSquare(int x, int y, Vector2 text){
		
		verts.Add( new Vector3 (x *scale, y  *scale, 0 ));
		verts.Add( new Vector3 ((x + 1) *scale, y  *scale, 0 ));
		verts.Add( new Vector3 ((x + 1) *scale, (y-1) *scale, 0 ));
		verts.Add( new Vector3 (x  *scale, (y-1) *scale, 0 ));

		Cube (text);
		
	}

	void Cube (Vector2 text) {
		
		tris.Add(squareCount*4);
		tris.Add((squareCount*4)+1);
		tris.Add((squareCount*4)+3);
		tris.Add((squareCount*4)+1);
		tris.Add((squareCount*4)+2);
		tris.Add((squareCount*4)+3);
		
		UV.Add( text * pixelSize );
		UV.Add( text * pixelSize );
		UV.Add( text * pixelSize );
		UV.Add( text * pixelSize );
		
		squareCount++; // Add this line
	}

	void UpdateMesh (Mesh m) {
		m.Clear ();
		m.vertices = verts.ToArray();
		m.triangles = tris.ToArray();
		m.uv = UV.ToArray();
		m.Optimize ();
		m.RecalculateNormals ();
		
		squareCount=0;
		verts.Clear();
		tris.Clear();
		UV.Clear();
	}

}
