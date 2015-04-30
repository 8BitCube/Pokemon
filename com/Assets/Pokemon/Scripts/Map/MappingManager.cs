using UnityEngine;
using System.Collections;

public class MappingManager : MonoBehaviour
{
	public GameObject MapPrefab;
	public TextAsset[] MapInfoArray;
	private GameObject[] m_MapArray;

	public void BuildWorld()
	{
		if(MapInfoArray.Length == 0)
		{
			Debug.LogError("Cannot continue, MapInfoArray must hold information");
			return;
		}

		m_MapArray = new GameObject[MapInfoArray.Length];

		for(int x = 0; x < m_MapArray.Length; x++)
		{
			bool _exsist = false;
			GameObject _tempMap = null;

			//Check our children to see if the map already exsists.
			foreach (Transform child in transform)
			{
				_exsist = (child.name == "Map " + x);
				_tempMap = child.gameObject;
				if(_exsist)
					break;
			}

			//Assign the values dependant on exsistance
			m_MapArray[x] = (_exsist) ? _tempMap : Instantiate(MapPrefab, Vector3.zero, Quaternion.identity) as GameObject;

			//Define inspector information
			m_MapArray[x].name = "Map " + x;
			m_MapArray[x].transform.parent = this.transform;

			//Link Mapdata and build the map
			MapData _mapData = m_MapArray[x].GetComponent<MapData>();
			_mapData.MapInfo = MapInfoArray[x];
			_mapData.LinkMap();
			m_MapArray[x].transform.position = new Vector3(_mapData.RootObject.width*x, 0, 0);
			_mapData.BuildMap();

			_tempMap = null;
		}
	}
}
