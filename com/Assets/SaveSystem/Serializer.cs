using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Serializer.
/// </summary>
public class Serializer
{
	/// <summary>
	/// Load the specified filename.
	/// </summary>
	/// <param name="filename">Filename.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T Load<T>(string filename) where T: class
	{
		if (File.Exists(filename))
		{
			try
			{
				using (Stream stream = File.OpenRead(filename))
				{
					BinaryFormatter formatter = new BinaryFormatter();
					return formatter.Deserialize(stream) as T;
				}
			}
			catch (Exception e)	{ Debug.Log(e.Message);	}
		}
		return default(T);
	}

	/// <summary>
	/// Save the specified filename and data.
	/// </summary>
	/// <param name="filename">Filename.</param>
	/// <param name="data">Data.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static void Save<T>(string filename, T data) where T: class
	{
		using (Stream stream = File.OpenWrite(filename))
		{    
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, data);
		}
	}
}

[Serializable()]
public struct Vector3S
{
	public float x;
	public float y;
	public float z;
}