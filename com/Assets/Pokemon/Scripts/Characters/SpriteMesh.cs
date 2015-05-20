using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteMesh 
{
	public List<Vector3> verts = new List<Vector3>();
	public List<int> tris = new List<int>();
	public List<Vector2> UV = new List<Vector2>();
	
	public Texture2D texture;
	int squareCount;

	public SpriteMesh() { }
	public SpriteMesh(Texture2D sprite) { texture=sprite; }

	Mesh tmp = new Mesh();

	public int downScale=2;
	public float depth=0.07f;

	public Mesh depthMesh(Vector2 pos, Vector2 size)
	{
		for(int x = 0; x < Mathf.RoundToInt(size.x/downScale); x ++)
		{
			for(int y = 0; y < Mathf.RoundToInt(size.y/downScale); y ++)
			{
				if(texture.GetPixel(Mathf.RoundToInt(pos.x)+(x*downScale),Mathf.RoundToInt(pos.y)+(y*downScale)).a!=0)
				{
					CubeSouth(x-Mathf.RoundToInt(size.x/(downScale*2)),y-Mathf.RoundToInt(size.y/(downScale*2)),0,pos + new Vector2(x*downScale,y*downScale));
					CubeNorth(x-Mathf.RoundToInt(size.x/(downScale*2)),y-Mathf.RoundToInt(size.y/(downScale*2)),0,pos + new Vector2(x*downScale,y*downScale));

					if(Mathf.RoundToInt(pos.x)+((x+1)*downScale)>=texture.width)
						CubeEast(x-Mathf.RoundToInt(size.x/(downScale*2)),y-Mathf.RoundToInt(size.y/(downScale*2)),0,pos + new Vector2(x*downScale,y*downScale));
					if(Mathf.RoundToInt(pos.x)+((x-1)*downScale)<=0)
						CubeWest(x-Mathf.RoundToInt(size.x/(downScale*2)),y-Mathf.RoundToInt(size.y/(downScale*2)),0,pos + new Vector2(x*downScale,y*downScale));
					if(Mathf.RoundToInt(pos.y)+((y+1)*downScale)>=texture.height)
						CubeTop(x-Mathf.RoundToInt(size.x/(downScale*2)),y-Mathf.RoundToInt(size.y/(downScale*2)),0,pos + new Vector2(x*downScale,y*downScale));
					if(Mathf.RoundToInt(pos.y)+((y-1)*downScale)<=0)
						CubeBot(x-Mathf.RoundToInt(size.x/(downScale*2)),y-Mathf.RoundToInt(size.y/(downScale*2)),0,pos + new Vector2(x*downScale,y*downScale));
					  

					if(texture.GetPixel(Mathf.RoundToInt(pos.x)+((x+1)*downScale),Mathf.RoundToInt(pos.y)+(y*downScale)).a==0)
						CubeEast(x-Mathf.RoundToInt(size.x/(downScale*2)),y-Mathf.RoundToInt(size.y/(downScale*2)),0,pos + new Vector2(x*downScale,y*downScale));

					if(texture.GetPixel(Mathf.RoundToInt(pos.x)+((x-1)*downScale),Mathf.RoundToInt(pos.y)+(y*downScale)).a==0)
						CubeWest(x-Mathf.RoundToInt(size.x/(downScale*2)),y-Mathf.RoundToInt(size.y/(downScale*2)),0,pos + new Vector2(x*downScale,y*downScale));

					if(texture.GetPixel(Mathf.RoundToInt(pos.x)+(x*downScale),Mathf.RoundToInt(pos.y)+((y+1)*downScale)).a==0)
						CubeTop(x-Mathf.RoundToInt(size.x/(downScale*2)),y-Mathf.RoundToInt(size.y/(downScale*2)),0,pos + new Vector2(x*downScale,y*downScale));

					if(texture.GetPixel(Mathf.RoundToInt(pos.x)+(x*downScale),Mathf.RoundToInt(pos.y)+((y-1)*downScale)).a==0)
						CubeBot(x-Mathf.RoundToInt(size.x/(downScale*2)),y-Mathf.RoundToInt(size.y/(downScale*2)),0,pos + new Vector2(x*downScale,y*downScale));
				}				
			}
		}

		UpdateMesh (tmp);
		tmp.name="SpriteMesh";
		return tmp;
	}
	public Mesh mesh(Vector2 pos, Vector2 size)
	{		
		for(int x = 0; x < Mathf.RoundToInt(size.x/2); x ++)
		{
			for(int y = 0; y < Mathf.RoundToInt(size.y/2); y ++)
			{
				if(texture.GetPixel((int)pos.x+x,(int)pos.y+y).a!=0)
					CubeSouth(x-Mathf.RoundToInt(size.x/2),y-Mathf.RoundToInt(size.y/2),0, pos + new Vector2(x,y));
			}
		}

		UpdateMesh (tmp);		
		tmp.name="SpriteMesh";
		return tmp;
	}

	public float scale=0.03f;
	public float pixelSizeX {
		get{
			return 1/(float)texture.width;
		}
	}
	public float pixelSizeY {
		get{
			return 1/(float)texture.height;
		}
	}

	void CubeSouth(int x, int y,float z, Vector2 text)
	{
		verts.Add(new Vector3 (x*scale, (y - 1)*scale, z));
		verts.Add(new Vector3 (x*scale, y*scale, z));
		verts.Add(new Vector3 ((x + 1)*scale, y*scale, z));
		verts.Add(new Vector3 ((x + 1)*scale, (y - 1)*scale, z));

		Cube (text);
	}
	void CubeNorth(int x, int y,float z, Vector2 text)
	{
		verts.Add(new Vector3 ((x + 1)*scale, (y - 1)*scale, z + depth));
		verts.Add(new Vector3 ((x + 1)*scale, y*scale, z + depth));
		verts.Add(new Vector3 (x*scale, y*scale, z + depth));
		verts.Add(new Vector3 (x*scale, (y - 1)*scale, z + depth));
		
		Cube (text);
	}
	void CubeBot(int x, int y,float z, Vector2 text)
	{
		verts.Add(new Vector3 (x*scale,  (y - 1)*scale,  z ));
		verts.Add(new Vector3 ((x + 1)*scale, (y - 1)*scale,  z ));
		verts.Add(new Vector3 ((x + 1)*scale, (y - 1)*scale,  z + depth));
		verts.Add(new Vector3 (x*scale, (y - 1)*scale,  z + depth));

		Cube (text);
	}
	void CubeTop(int x, int y,float z, Vector2 text)
	{
		verts.Add(new Vector3 (x*scale,  y*scale,  z + depth));
		verts.Add(new Vector3 ((x + 1)*scale, y*scale,  z + depth));
		verts.Add(new Vector3 ((x + 1)*scale, y*scale,  z ));
		verts.Add(new Vector3 (x*scale,  y*scale,  z ));
		
		Cube (text);
	}
	void CubeWest(int x, int y,float z, Vector2 text)
	{
		verts.Add(new Vector3 (x*scale, (y - 1)*scale, z + depth));
		verts.Add(new Vector3 (x*scale, y*scale, z + depth));
		verts.Add(new Vector3 (x*scale, y*scale, z));
		verts.Add(new Vector3 (x*scale, (y - 1)*scale, z));
		
		Cube (text);
	}
	void CubeEast(int x, int y,float z, Vector2 text)
	{
		verts.Add(new Vector3 ((x + 1)*scale, (y - 1)*scale, z));
		verts.Add(new Vector3 ((x + 1)*scale, y*scale, z));
		verts.Add(new Vector3 ((x + 1)*scale, y*scale, z + depth));
		verts.Add(new Vector3 ((x + 1)*scale, (y - 1)*scale, z + depth));
		
		Cube (text);
	}

	void Cube (Vector2 text) 
	{		
		tris.Add(squareCount*4);
		tris.Add((squareCount*4)+1);
		tris.Add((squareCount*4)+3);
		tris.Add((squareCount*4)+1);
		tris.Add((squareCount*4)+2);
		tris.Add((squareCount*4)+3);
		
		UV.Add( new Vector2(text.x*pixelSizeX,text.y*pixelSizeY) +new Vector2(pixelSizeX/2,pixelSizeY/2 ));
		UV.Add( new Vector2(text.x*pixelSizeX,text.y*pixelSizeY) +new Vector2(pixelSizeX/2,pixelSizeY/2 ));
		UV.Add( new Vector2(text.x*pixelSizeX,text.y*pixelSizeY) +new Vector2(pixelSizeX/2,pixelSizeY/2 ));
		UV.Add( new Vector2(text.x*pixelSizeX,text.y*pixelSizeY) +new Vector2(pixelSizeX/2,pixelSizeY/2 ));

		squareCount++; // Add this line
	}

	void UpdateMesh (Mesh m) 
	{
		m.Clear ();
		m.vertices = verts.ToArray();
		m.triangles = tris.ToArray();
		m.uv = UV.ToArray();
		m.Optimize ();
		m.RecalculateNormals ();
		
		squareCount=0;
		verts.Clear();
		tris.Clear();
		UV.Clear();	}

}
