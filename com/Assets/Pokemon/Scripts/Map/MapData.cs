using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MapData : MonoBehaviour
{
	public TextAsset MapInfo;
	public RootObject RootObject;
	
	public void LinkMap()
	{
		//First we read in our map data serializing our json script
		RootObject = JsonMapper.ToObject<RootObject>(MapInfo.ToString());
	}
	
	public void BuildMap()
	{			
		int texWidth = RootObject.layers[0].width * RootObject.tilewidth;
		int texHeight = RootObject.layers[0].height * RootObject.tileheight;
		
		Texture2D texture = new Texture2D(texWidth, texHeight);
		Renderer renderer = this.GetComponent<Renderer> ();
		var tempMaterial = new Material(renderer.sharedMaterial);
		tempMaterial.mainTexture = texture;
		renderer.sharedMaterial = tempMaterial;

		for(int x = 0; x < RootObject.layers.Count; x ++)
		{
			for(int y = 0; y < RootObject.tilesets.Count; y ++) //Testing atm, leave here
			{
				//Once we have read the map data and assigned our values accordingly, we then build the mesh.
				BuildMesh(RootObject, x, y);
				
				//After the mesh as been built, we assign our texture.
				BuildTexture(RootObject, x, y);
			}
		}
	}
	
	/// <summary>
	/// Builds the mesh.
	/// </summary>
	/// <param name="aRootObject">A root object.</param>
	/// <param name="aLayer">A layer.</param>
	public void BuildMesh(RootObject aRootObject, int aLayer, int aTileSet)
	{
		aRootObject.width = aRootObject.height = 1;
		int numTiles = aRootObject.width * aRootObject.height;
		int numTris = numTiles * 2;
		
		int vsize_x = aRootObject.width + 1;
		int vsize_z = aRootObject.height + 1;
		int numVerts = vsize_x * vsize_z;
		
		// Generate the mesh data
		Vector3[] vertices = new Vector3[ numVerts ];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];
		
		int[] triangles = new int[ numTris * 3 ];
		
		int x, z;
		for(z=0; z < vsize_z; z++) 
		{
			for(x=0; x < vsize_x; x++) 
			{
				vertices[ z * vsize_x + x ] = new Vector3( x*aRootObject.layers[aLayer].width, 0, -z*aRootObject.layers[aLayer].height );
				normals[ z * vsize_x + x ] = Vector3.up;
				uv[ z * vsize_x + x ] = new Vector2( (float)x / (aRootObject.width), 1f-(float)z / (aRootObject.height) );
			}
		}
		
		for(z=0; z < aRootObject.height; z++) 
		{
			for(x=0; x < aRootObject.width; x++)
			{
				int squareIndex = z * aRootObject.width + x;
				int triOffset = squareIndex * 6;
				triangles[triOffset + 0] = z * vsize_x + x + 		   0;
				triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 0;
				triangles[triOffset + 1] = z * vsize_x + x + vsize_x + 1;
				
				triangles[triOffset + 3] = z * vsize_x + x + 		   0;
				triangles[triOffset + 5] = z * vsize_x + x + vsize_x + 1;
				triangles[triOffset + 4] = z * vsize_x + x + 		   1;
			}
		}
		
		// Create a new Mesh and populate with the data
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
		
		// Assign our mesh to our filter/renderer/collider
		MeshFilter mesh_filter = GetComponent<MeshFilter>();
		MeshCollider mesh_collider = GetComponent<MeshCollider>();
		
		mesh_filter.mesh = mesh;
		mesh_collider.sharedMesh = mesh;
	}
	
	public void BuildTexture(RootObject aRootObject, int aLayer, int aTileSet)
	{		
		string value = aRootObject.tilesets[aTileSet].image;
		
		value = value.Replace(".png", "");
		value = value.Replace(".PNG", "");
		value = value.Replace("../",  "");
		
		Texture2D _tex = Resources.Load(value) as Texture2D;
		Renderer renderer = this.GetComponent<Renderer> ();
		int texWidth = aRootObject.layers[aLayer].width * aRootObject.tilewidth;
		int texHeight = aRootObject.layers[aLayer].height * aRootObject.tileheight;
		
		Texture2D texture = new Texture2D(texWidth, texHeight);	
		var tempMaterial = new Material(renderer.sharedMaterial);
		
		int g = 0;
		
		Color[][] tiles = ChopUpTiles(_tex);
		
		for(int y=aRootObject.layers[aLayer].height-1; y>=0; y--) 
		{
			for(int x=0; x<aRootObject.layers[aLayer].width; x++) 
			{
				if((aRootObject.layers[aLayer].data[g]) < tiles.Length)
				{
					Texture2D temp = tempMaterial.mainTexture as Texture2D;
					Color[] aBaseTexturePixels = temp.GetPixels(x*aRootObject.tilewidth, y*aRootObject.tileheight, aRootObject.tilewidth, aRootObject.tileheight);
					Color[] aCopyTexturePixels = (aRootObject.layers[aLayer].data[g])-1 >= 0 ? tiles[ (aRootObject.layers[aLayer].data[g])-1] : aBaseTexturePixels;


					for(int i = 0; i < aCopyTexturePixels.Length; i++)
					{
						if(aCopyTexturePixels[i].a == 0)
							aCopyTexturePixels[i] = aBaseTexturePixels[i];
					}
					texture.SetPixels(x*aRootObject.tilewidth, y*aRootObject.tileheight, aRootObject.tilewidth, aRootObject.tileheight, aCopyTexturePixels);
				}
				g++;
			}
		}

		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply();
		
		tempMaterial.mainTexture = texture;
		renderer.sharedMaterial = tempMaterial;
	}
	
	Color[][] ChopUpTiles(Texture2D aTerrainTiles) 
	{
		int numTilesPerRow = aTerrainTiles.width / 32;
		int numRows = aTerrainTiles.height /32;
		
		Color[][] tiles = new Color[numTilesPerRow*numRows][];
		int count = 0;
		
		for(int y=numRows-1; y>=0; y--) 
		{
			for(int x=0; x<numTilesPerRow; x++) 
			{
				tiles[count] = aTerrainTiles.GetPixels( (x)*32, (y)*32, 32, 32 );
				count++;
			}
		}
		
		return tiles;
	}
}	