using System.Collections.Generic;
using PathFinding.GridStructure;
using PathFinding.General;
using System;

namespace PathFinding.Dijkstra
{
    public class DijkstraController
    {

        DijkstraPath currentPath;

        IGrid worldGrid;

        public int MaxIterations { get; set; }

        public DijkstraController(IGrid grid)
        {
            worldGrid = grid;
            currentPath = null;
            MaxIterations = -1;
        }

        public void setPath(DijkstraPath newPath)
        {
            currentPath = newPath;
        }

        //Attempts to find the path. True if it finds the path, False if it takes too long.
        //If it does not complete, then will generally continue in the next frame.
        public bool findPath()
        {
            //TODO: Is from AmitP website?
            //while Q is not empty:                 // The main loop
            //	u := Q.extract_min()              // Remove and return best vertex
            //	mark u as scanned
            //	for each neighbor v of u:
            //	    if v is not yet scanned:
            //	        alt = dist[u] + length(u, v) 
            //	        if alt < dist[v]
            //	            dist[v] := alt
            //	            previous[v] := u
            //	            Q.decrease_priority(v,alt)
            //	        end if
            //	    end if
            //	end for
            //end while
            //return previous[]
            PathNode current = null;
            bool openListPopulated = currentPath.isOpenListPopulated();
            bool reachedTarget = false;
            int iterations = 0;
            while (openListPopulated && !reachedTarget && (iterations < MaxIterations || MaxIterations == -1))
            {
                current = currentPath.getHighestPriorityNode();
                currentPath.setCurrentList(current, CellStatus.CLOSED);

                List<PathNode> neighbours = worldGrid.getNeighbours(current);
                foreach (PathNode neighbour in neighbours)
                {
                    if (currentPath.currentList(neighbour) != CellStatus.CLOSED)
                    {
                        float cost = Math.Abs(currentPath.getGScore(current) + worldGrid.getMovementCost(current, neighbour));
                        if (cost < currentPath.getGScore(neighbour))
                        {
                            currentPath.setGScore(neighbour, cost);
                            currentPath.setNodeParent(neighbour, current);
                            currentPath.adjustNodePriority(neighbour, cost);
                        }
                    }
                }

                openListPopulated = currentPath.isOpenListPopulated();
                reachedTarget = currentPath.reachedTarget();
                iterations++;
            }

            currentPath.Complete = reachedTarget;

            return reachedTarget;
        }
    }
}