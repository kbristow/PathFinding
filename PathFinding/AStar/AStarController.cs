using PathFinding.Collections;
using PathFinding.General;
using PathFinding.GridStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinding.AStar
{
    public class AStarController
    {

        AStarPath currentPath;

        IGrid worldGrid;

        public int MaxIterations { get; set; }

        public AStarController(IGrid grid)
        {
            worldGrid = grid;
            currentPath = null;
            MaxIterations = -1;
        }

        public void setPath(AStarPath newPath)
        {
            currentPath = newPath;
        }

        //Attempts to find the path. True if it finds the path, False if it takes too long.
        //If it does not complete, then will generally continue in the next frame.
        public bool findPath()
        {
            PathNode current = null;
            bool openListPopulated = currentPath.isOpenListPopulated();
            bool reachedTarget = false;
            int iterations = 0;
            while (openListPopulated && !reachedTarget && (iterations < MaxIterations || MaxIterations == -1))
            {
                //current = remove lowest rank item from OPEN
                current = currentPath.getHighestPriorityNode();
                //add current to CLOSED
                currentPath.addToClosedList(current, currentPath.getGScore(current));

                List<PathNode> neighbours = worldGrid.getNeighbours(current);

                //for neighbors of current:
                foreach (PathNode neighbour in neighbours)
                {
                    //  cost = g(current) + movementcost(current, neighbor)
                    float cost = currentPath.getGScore(current) + worldGrid.getMovementCost(current, neighbour);

                    CellStatus currentList = currentPath.currentList(neighbour);

                    //  if neighbor in OPEN and cost less than g(neighbor):
                    //    remove neighbor from OPEN, because new path is better
                    if (currentList == CellStatus.OPEN && cost < currentPath.getGScore(neighbour))
                    {
                        float heuristic = currentPath.Heuristic.getHeuristicValue(neighbour.X, neighbour.Y, currentPath.TargetX, currentPath.TargetY);
                        currentPath.setGScore(neighbour, cost);
                        currentPath.setNodeParent(neighbour, current);
                        currentPath.adjustNodePriority(neighbour, cost + heuristic);
                    }

                    //  if neighbor in CLOSED and cost less than g(neighbor): 
                    //    remove neighbor from CLOSED
                    if (currentList == CellStatus.CLOSED && cost < currentPath.getGScore(neighbour))
                    {
                        currentPath.removeFromClosedList(neighbour);
                        currentList = CellStatus.NONE;
                    }

                    //  if neighbor not in OPEN and neighbor not in CLOSED:
                    //    set g(neighbor) to cost
                    //    add neighbor to OPEN
                    //    set priority queue rank to g(neighbor) + h(neighbor)
                    //    set neighbor's parent to current
                    if (currentList == CellStatus.NONE)
                    {
                        float heuristic = currentPath.Heuristic.getHeuristicValue(neighbour.X, neighbour.Y, currentPath.TargetX, currentPath.TargetY);
                        currentPath.setGScore(neighbour, cost);
                        currentPath.addToOpenList(neighbour, cost + heuristic);
                        currentPath.setNodeParent(neighbour, current);
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