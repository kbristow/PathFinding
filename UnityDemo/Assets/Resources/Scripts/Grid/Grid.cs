using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Test;
using PathFinding.General;

public class Grid : MonoBehaviour
{

    public GameObject control;

    Dictionary<Vector2, GameObject> obstacles = new Dictionary<Vector2, GameObject>();

    // Use this for initialization
    void Start()
    {
        //TODO: Was done with Vectrosity but removed for public repo, build new solution, maybe use GL Lines
        //if (showGrid)
        //{
        //    float cellSize = Map.CELL_SIZE;

        //for (int i = 0; i <= Map.GRID_WIDTH / cellSize; i++)
        //{
        //    VectorLine horizontalLine = VectorLine.SetLine3D(Color.grey, new Vector3(i * cellSize, 0, 0), new Vector3(i * cellSize, 0, Map.GRID_HEIGHT));
        //    horizontalLine.SetWidth(1);
        //    horizontalLine.Draw();
        //}

        //for (int i = 0; i <= Map.GRID_HEIGHT / cellSize; i++)
        //{
        //    VectorLine verticalLine = VectorLine.SetLine3D(Color.gray, new Vector3(0, 0, i * cellSize), new Vector3(Map.GRID_WIDTH, 0, i * cellSize));
        //    verticalLine.SetWidth(1);
        //    verticalLine.Draw();
        //}
        //}
    }


    void Update()
    {
        //check if the left mouse has been pressed down this frame 
        if (Input.GetMouseButton(1))
        {
            //empty RaycastHit object which raycast puts the hit details into 
            RaycastHit hit = new RaycastHit();
            //ray shooting out of the camera from where the mouse is 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitLocation = hit.point;
                if (Input.GetMouseButtonDown(1))
                {
                    createOrDestroyObstacle(hitLocation);
                }
            }
        }
        Map.processPaths();
    }

    private void createOrDestroyObstacle(Vector3 hitLocation)
    {
        Vector2 gridPoint = Map.convertMapToGridPoint(hitLocation);
        if (Map.Grid.Grid[(int)gridPoint.x, (int)gridPoint.y].Walkable)
        {
            float x = gridPoint.x * Map.CELL_SIZE + Map.CELL_SIZE / 2;
            float y = gridPoint.y * Map.CELL_SIZE + Map.CELL_SIZE / 2;

            GameObject instance = (GameObject)Instantiate(Resources.Load("Prefabs/Obstacle", typeof(GameObject)));
            instance.transform.localScale = new Vector3(Map.CELL_SIZE, 1, Map.CELL_SIZE);
            instance.transform.position = new Vector3(x, instance.transform.position.y, y);

            Map.Grid.setObstacle((int)gridPoint.x, (int)gridPoint.y);
            obstacles.Add(gridPoint, instance);
        }
        else
        {
            Map.Grid.removeObstacle((int)gridPoint.x, (int)gridPoint.y);
            GameObject obstacle = obstacles[gridPoint];
            UnityEngine.GameObject.Destroy(obstacle);
            obstacles.Remove(gridPoint);
        }
    }
}