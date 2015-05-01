using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteMesh {
	
	public List<Vector3> verts = new List<Vector3>();

	public List<int> tris = new List<int>();

	public List<Vector2> UV = new List<Vector2>();
	
	Texture2D texture;

	int squareCount;

	public SpriteMesh() { }
	public SpriteMesh(Texture2D sprite){
		texture=sprite;
	}

	Mesh tmp = new Mesh();

	public Mesh depthMesh(Vector2 pos, Vector2 size){

		for(int x = 0; x < Mathf.RoundToInt(size.x/2); x ++){
			for(int y = 0; y < Mathf.RoundToInt(size.y/2); y ++){

				if(texture.GetPixel(Mathf.RoundToInt(pos.x)+(x*2),Mathf.RoundToInt(pos.y)+(y*2)).a!=0){
					CubeSouth(x-Mathf.RoundToInt(size.x/4),y-Mathf.RoundToInt(size.y/4),0,pos + new Vector2(x*2,y*2));
					CubeNorth(x-Mathf.RoundToInt(size.x/4),y-Mathf.RoundToInt(size.y/4),0,pos + new Vector2(x*2,y*2));

					if(texture.GetPixel(Mathf.RoundToInt(pos.x)+((x+1)*2),Mathf.RoundToInt(pos.y)+(y*2)).a==0){
						CubeEast(x-Mathf.RoundToInt(size.x/4),y-Mathf.RoundToInt(size.y/4),0,pos + new Vector2(x*2,y*2));
					}
					if(texture.GetPixel(Mathf.RoundToInt(pos.x)+((x-1)*2),Mathf.RoundToInt(pos.y)+(y*2)).a==0){
						CubeWest(x-Mathf.RoundToInt(size.x/4),y-Mathf.RoundToInt(size.y/4),0,pos + new Vector2(x*2,y*2));
					}
					if(texture.GetPixel(Mathf.RoundToInt(pos.x)+(x*2),Mathf.RoundToInt(pos.y)+((y+1)*2)).a==0){
						CubeTop(x-Mathf.RoundToInt(size.x/4),y-Mathf.RoundToInt(size.y/4),0,pos + new Vector2(x*2,y*2));
					}
					if(texture.GetPixel(Mathf.RoundToInt(pos.x)+(x*2),Mathf.RoundToInt(pos.y)+((y-1)*2)).a==0){
						CubeBot(x-Mathf.RoundToInt(size.x/4),y-Mathf.RoundToInt(size.y/4),0,pos + new Vector2(x*2,y*2));
					}

				}
				
			}
		}

		UpdateMesh (tmp);

		return tmp;
	}
	public Mesh mesh(Vector2 pos, Vector2 size){
		
		for(int x = 0; x < Mathf.RoundToInt(size.x/2); x ++){
			for(int y = 0; y < Mathf.RoundToInt(size.y/2); y ++){
				
				if(texture.GetPixel((int)pos.x+x,(int)pos.y+y).a!=0){
					CubeSouth(x-Mathf.RoundToInt(size.x/2),y-Mathf.RoundToInt(size.y/2),0, pos + new Vector2(x,y));
				}
				
			}
		}

		UpdateMesh (tmp);
		
		return tmp;
	}

	public float scale=0.03f;
	public float pixelSize = 0.00390625f;

	void CubeSouth(int x, int y,float z, Vector2 text){
		verts.Add(new Vector3 (x*scale, (y - 1)*scale, z));
		verts.Add(new Vector3 (x*scale, y*scale, z));
		verts.Add(new Vector3 ((x + 1)*scale, y*scale, z));
		verts.Add(new Vector3 ((x + 1)*scale, (y - 1)*scale, z));

		Cube (text);
	}
	void CubeNorth(int x, int y,float z, Vector2 text){
		verts.Add(new Vector3 ((x + 1)*scale, (y - 1)*scale, z + 0.1f));
		verts.Add(new Vector3 ((x + 1)*scale, y*scale, z + 0.1f));
		verts.Add(new Vector3 (x*scale, y*scale, z + 0.1f));
		verts.Add(new Vector3 (x*scale, (y - 1)*scale, z + 0.1f));
		
		Cube (text);
	}
	void CubeBot(int x, int y,float z, Vector2 text){
		verts.Add(new Vector3 (x*scale,  (y - 1)*scale,  z ));
		verts.Add(new Vector3 ((x + 1)*scale, (y - 1)*scale,  z ));
		verts.Add(new Vector3 ((x + 1)*scale, (y - 1)*scale,  z + 0.1f));
		verts.Add(new Vector3 (x*scale, (y - 1)*scale,  z + 0.1f));

		Cube (text);
	}
	void CubeTop(int x, int y,float z, Vector2 text){
		verts.Add(new Vector3 (x*scale,  y*scale,  z + 0.1f));
		verts.Add(new Vector3 ((x + 1)*scale, y*scale,  z + 0.1f));
		verts.Add(new Vector3 ((x + 1)*scale, y*scale,  z ));
		verts.Add(new Vector3 (x*scale,  y*scale,  z ));
		
		Cube (text);
	}
	void CubeWest(int x, int y,float z, Vector2 text){
		verts.Add(new Vector3 (x*scale, (y - 1)*scale, z + 0.1f));
		verts.Add(new Vector3 (x*scale, y*scale, z + 0.1f));
		verts.Add(new Vector3 (x*scale, y*scale, z));
		verts.Add(new Vector3 (x*scale, (y - 1)*scale, z));
		
		Cube (text);
	}
	void CubeEast(int x, int y,float z, Vector2 text){
		verts.Add(new Vector3 ((x + 1)*scale, (y - 1)*scale, z));
		verts.Add(new Vector3 ((x + 1)*scale, y*scale, z));
		verts.Add(new Vector3 ((x + 1)*scale, y*scale, z + 0.1f));
		verts.Add(new Vector3 ((x + 1)*scale, (y - 1)*scale, z + 0.1f));
		
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
