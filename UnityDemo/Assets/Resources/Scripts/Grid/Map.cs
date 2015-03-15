using System;
using UnityEngine;
using System.Collections.Generic;
using PathFinding.General;
using Test;
using PathFinding.AStar;
using PathFinding.Dijkstra;

public class Map
{
	public static float CELL_SIZE = 8;
	public static float GRID_WIDTH = 200;
	public static float GRID_HEIGHT = 200;

	public static TestGrid Grid;
	static PathManager pathMan;

	public static void init(){
		Grid = new TestGrid((int)(GRID_WIDTH/CELL_SIZE), (int)(GRID_HEIGHT/CELL_SIZE));

		pathMan = new PathManager (Grid); 
		pathMan.MaxIterations = 100;

		ResourceManager.addNewResource(4,16);
		ResourceManager.addNewResource(17,7);
		ResourceManager.addNewResource(18,3);
		ResourceManager.addNewResource(19,2);
		ResourceManager.addNewResource(2,15);
		ResourceManager.addNewResource(12,8);
		ResourceManager.addNewResource(11,11);
	}

	public static void processPaths(){
		pathMan.processPaths();
	}

	public static PathContainer addDirectedPath(int startX, int startY, int targetX, int targetY, IHeuristic heuristic){
		return pathMan.addDirectedPath(startX, startY, targetX, targetY, heuristic);
	}

	public static PathContainer addUnDirectedPath(int startX, int startY, ILocationTest locationTest){
		return pathMan.addUnDirectedPath(startX, startY, locationTest);
	}

	public static Vector2 convertMapToGridPoint(Vector3 mapPoint){
		Vector2 gridPoint = Vector2.zero;
		gridPoint.x = (int) (mapPoint.x/CELL_SIZE);
		gridPoint.y = (int) (mapPoint.z/CELL_SIZE);
		return gridPoint;
	}

	public static Vector3 convertGridToMapPoint(Vector2 gridPoint){
		Vector3 mapPoint = Vector2.zero;
		mapPoint.x = gridPoint.x * CELL_SIZE + CELL_SIZE/2;
		mapPoint.z = gridPoint.y * CELL_SIZE + CELL_SIZE/2;
		return mapPoint;
	}

	public static Vector3[] convertGridToMapPath(List<PathNode> gridPath){
		Vector3[] actualPath = new Vector3[gridPath.Count];

		for (int i = 0; i < gridPath.Count; i ++) {
			PathNode pNode = gridPath [i];

			if(pNode==null){
				int k = 0;//used for a break point
			}
			actualPath [i] = new Vector3 (pNode.X * CELL_SIZE + CELL_SIZE / 2, 0, pNode.Y * CELL_SIZE + CELL_SIZE / 2);
		}

		return actualPath;
	}
}