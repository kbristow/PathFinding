using System.Collections.Generic;
using PathFinding.AStar;
using PathFinding.GridStructure;
using PathFinding.Dijkstra;

namespace PathFinding.General
{

    //Wraps all path finding functionality. Should only need 1 of these to manage paths for all objects in a game scene.
    public class PathManager
    {

        //Paths to be processed by the AStarController
        List<AStarPath> directedPaths;
        //Paths to be processed by the DijkstraController
        List<DijkstraPath> undirectedPaths;
        AStarController aStarController;
        DijkstraController dijkstraController;

        public int Width { set; get; }
        public int Height { set; get; }

        //Max time that can be spent on trying to find paths per frame 
        public long MaxTime { get; set; }

        //Max number of loop interations allowed to try find a path per path per frame
        private int maxIterations = -1;
        public int MaxIterations
        {
            set
            {
                aStarController.MaxIterations = value;
                dijkstraController.MaxIterations = value;
                maxIterations = value;
            }

            get
            {
                return maxIterations;
            }
        }

        public PathManager(IGrid grid)
        {
            directedPaths = new List<AStarPath>();
            undirectedPaths = new List<DijkstraPath>();
            aStarController = new AStarController(grid);
            dijkstraController = new DijkstraController(grid);
            Width = grid.Width;
            Height = grid.Height;
            MaxTime = (long)((10000000.0f / 30) * (20.0f / 100));
        }

        //Adds a target location to find a path to using the provided heuristic. Uses AStar to move to the location
        public PathContainer addDirectedPath(int startX, int startY, int targetX, int targetY, IHeuristic heuristic)
        {
            AStarPath path = new AStarPath(startX, startY, targetX, targetY, Width, Height, heuristic);
            if (startX != targetX || startY != targetY)
            {
                directedPaths.Add(path);
            }
            else
            {
                path.Complete = true;
            }
            return path;
        }

        //Adds a path to the nearest location to startX,startY that meets locationTest criteria
        public PathContainer addUnDirectedPath(int startX, int startY, ILocationTest locationTest)
        {
            DijkstraPath path = new DijkstraPath(startX, startY, Width, Height, locationTest);
            undirectedPaths.Add(path);
            return path;
        }

        //Processes the loaded paths. Uses time splicing so paths may take multiple processPaths to be found
        public void processPaths()
        {
            long startTime = System.DateTime.Now.Ticks;
            while ((directedPaths.Count > 0 || undirectedPaths.Count > 0) && System.DateTime.Now.Ticks - startTime < MaxTime)
            {
                if (directedPaths.Count > 0)
                {
                    AStarPath currentPath = directedPaths[0];
                    directedPaths.RemoveAt(0);
                    aStarController.setPath(currentPath);
                    aStarController.findPath();
                    if (!currentPath.Complete)
                    {
                        directedPaths.Add(currentPath);
                    }
                }

                if (undirectedPaths.Count > 0)
                {
                    DijkstraPath currentPath = undirectedPaths[0];
                    undirectedPaths.RemoveAt(0);
                    dijkstraController.setPath(currentPath);
                    dijkstraController.findPath();
                    if (!currentPath.Complete)
                    {
                        undirectedPaths.Add(currentPath);
                    }
                }
            }
        }
    }
}