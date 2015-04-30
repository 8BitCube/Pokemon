﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RootObject
{
	public int height { get; set; }
	public List<Layer> layers { get; set; }
	public int nextobjectid { get; set; }
	public string orientation { get; set; }
	public Properties properties { get; set; }
	public string renderorder { get; set; }
	public int tileheight { get; set; }
	public List<Tileset> tilesets { get; set; }
	public int tilewidth { get; set; }
	public int version { get; set; }
	public int width { get; set; }
}

public class Layer
{
	public List<int> data { get; set; }
	public int height { get; set; }
	public string name { get; set; }
	public int opacity { get; set; }
	public string type { get; set; }
	public bool visible { get; set; }
	public int width { get; set; }
	public int x { get; set; }
	public int y { get; set; }
}

public class Properties { }
public class Properties2 { }

public class Tileset
{
	public int firstgid { get; set; }
	public string image { get; set; }
	public int imageheight { get; set; }
	public int imagewidth { get; set; }
	public int margin { get; set; }
	public string name { get; set; }
	public Properties2 properties { get; set; }
	public int spacing { get; set; }
	public int tileheight { get; set; }
	public int tilewidth { get; set; }
}



