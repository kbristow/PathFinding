using System;
using UnityEngine;
using PathFinding.General;
using Test;
using System.Collections.Generic;

//Tries to find resource objects and collect them
public class TestBot2:MonoBehaviour
{
	Vector2 target;
	PathContainer path;
	bool moving = false;

	public static List<Vector2> testbot2Targets = new List<Vector2>();

	void Start(){
		setNewPath();
	}

	void Update(){
		doPath();
	}

	void setNewPath(){
		Vector2 currentLocation = Map.convertMapToGridPoint(transform.position);

		path = Map.addUnDirectedPath((int)currentLocation.x, (int)currentLocation.y, new TestLocationTest(Map.Grid, "Res"));
	}


	public void doPath(){		
		if (path.Complete && !moving) {
			List<PathNode> builtPath = path.getPath ();

			MoveToTarget movement = (GetComponent ("MoveToTarget") as MoveToTarget);
			movement.setTargetsFromArray (Map.convertGridToMapPath(builtPath));
			moving = true;

			PathNode targetNode = builtPath[builtPath.Count - 1];
			target.x = targetNode.X;
			target.y = targetNode.Y;
			//testbot2Targets.Add(target);
		}
		else if (moving){
			moving = !reachedTarget();
			if (!moving){
				ResourceManager.removeResource((int)target.x, (int)target.y);
				int x = 0;
				int y = 0;
				Vector2 currentLocation = Map.convertMapToGridPoint(transform.position);
				do{
					x = UnityEngine.Random.Range(0, Map.Grid.Width);
					y = UnityEngine.Random.Range(0, Map.Grid.Height);
				}while(Map.Grid.Grid[x,y].NodeType == "Res" || !Map.Grid.Grid[x,y].Walkable || (x == currentLocation.x && y == currentLocation.y));

				ResourceManager.addNewResource(x,y);
				testbot2Targets.Remove(target);
				setNewPath();
			}
		}
	}

	public bool reachedTarget(){
		bool flag = false;
		Vector2 currentLocation = Map.convertMapToGridPoint(transform.position);
		if (currentLocation == target){
			flag = true;
		}
		return flag;
	}
}