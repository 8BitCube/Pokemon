using UnityEngine;
using System.IO;

/// Author: Andrew Mills
/// Last Edit: 3.1.2015
/// Definition: This GameManager script will handle universal 
/// 			things like points and number of levels completed
public class GameManager 
{
	private static GameManager _instance;
	public static GameManager Instance { get { return _instance ?? (_instance = new GameManager ()); } }
	
	public int Points { get; private set; }
	
	//Leave Private, no one else should instantiate this class
	private GameManager () { }
	
	public void Reset()
	{ Points = 0; }
	
	public void ResetPoints(int aPoints)
	{ Points = aPoints; }
	
	public void AddPoints(int aPointsToAdd)
	{ Points += aPointsToAdd; }
	
	public void ResetAllValues()
	{
		
	}
}
