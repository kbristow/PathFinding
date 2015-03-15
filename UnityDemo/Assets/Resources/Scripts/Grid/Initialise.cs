using UnityEngine;
using System.Collections;

public class Initialise : MonoBehaviour {

	// Use this for initialization, it is set up to run first in the script execution list
	void Start () {
		Map.init();
		for (int i = 0; i < 4; i ++){
			GameObject instance = (GameObject)Instantiate(Resources.Load("Prefabs/TestBot2", typeof(GameObject)));
			int x = (int)Random.Range(0,Map.GRID_WIDTH);
			int y = (int)Random.Range(0,Map.GRID_HEIGHT);
			Vector3 newMapPoint = Map.convertGridToMapPoint(new Vector2(x,y));
			newMapPoint.y = instance.transform.position.y;
			//This next line breaks for some reason.
			//instance.transform.position = newMapPoint;
			//Map.Grid.Grid[x,y].NodeObject = instance;
		}
	}
}