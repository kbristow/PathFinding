using System;
using UnityEngine;

//Loads and removes resource prefabs from the scene
public class ResourceManager
{
	public static void addNewResource(int x, int y){
		Map.Grid.Grid[x,y].NodeType = "Res";
		Resources.Load("Prefabs/Resource", typeof(GameObject));
		GameObject instance = (GameObject)UnityEngine.GameObject.Instantiate(Resources.Load("Prefabs/Resource", typeof(GameObject)));
		Vector3 newMapPoint = Map.convertGridToMapPoint(new Vector2(x,y));
		newMapPoint.y = instance.transform.position.y;
		instance.transform.position = newMapPoint;
		Map.Grid.Grid[x,y].NodeObject = instance;
	}

	public static bool removeResource(int x, int y){
		if (Map.Grid.Grid[x,y].NodeType == "Res" && Map.Grid.Grid[x,y].NodeObject != null){
			UnityEngine.GameObject.Destroy(Map.Grid.Grid[x,y].NodeObject);
			Map.Grid.Grid[x,y].NodeObject = null;
			Map.Grid.Grid[x,y].NodeType = "None";
			return true;
		}
		return false;
	}
}