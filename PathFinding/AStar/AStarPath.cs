using PathFinding.Collections;
using PathFinding.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinding.AStar
{
    //Maintains the state of the path search from frame to frame. Allows for time splicing. Most of the class 
    //is just the implementation of the functions used in the AStar Algo
    public class AStarPath : PathContainer
    {
        public int StartX, StartY;
        public int TargetX, TargetY;
        public BinaryHeap<PathNode> OpenList { get; set; }
        public Dictionary<String, BinaryNode<PathNode>> ClosedList { get; set; }
        public IHeuristic Heuristic { get; set; }
        public bool Complete { get; set; }
        private PathNode goal;

        public CellState[,] CellStates;

        public AStarPath(int startX, int startY, int targetX, int targetY, int gridWidth, int gridHeight, IHeuristic heuristic)
        {
            Complete = false;
            StartX = startX;
            StartY = startY;
            TargetX = targetX;
            TargetY = targetY;

            Heuristic = heuristic;

            CellStates = new CellState[gridWidth, gridHeight];
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    CellStates[i, j] = new CellState();
                }
            }
            OpenList = new BinaryHeap<PathNode>();
            PathNode pNode = new PathNode(startX, startY);
            addToOpenList(pNode, 0);
            CellStates[startX, startY].Parent = pNode;
            ClosedList = new Dictionary<String, BinaryNode<PathNode>>();
        }

        public void addToOpenList(PathNode node, float fScore)
        {
            BinaryNode<PathNode> newBinaryNode = new BinaryNode<PathNode>(node, fScore);
            OpenList.insertNode(newBinaryNode);
            CellStates[node.X, node.Y].ListState = CellStatus.OPEN;
        }

        public PathNode getHighestPriorityNode()
        {
            PathNode node = OpenList.pop().Node;
            CellStates[node.X, node.Y].ListState = CellStatus.NONE;
            return node;
        }

        public PathNode peakHighestPriorityNode()
        {
            BinaryNode<PathNode> node = OpenList.peek();
            if (node != null)
            {
                return node.Node;
            }
            return null;
        }

        public void addToClosedList(PathNode node, float gScore)
        {
            BinaryNode<PathNode> newBinaryNode = new BinaryNode<PathNode>(node, gScore);
            ClosedList[node.generateID()] = newBinaryNode;
            CellStates[node.X, node.Y].ListState = CellStatus.CLOSED;
            CellStates[node.X, node.Y].GScore = gScore;
        }

        public void removeFromClosedList(PathNode node)
        {
            ClosedList[node.generateID()] = null;
            CellStates[node.X, node.Y].ListState = CellStatus.NONE;
        }

        public CellStatus currentList(PathNode node)
        {
            return CellStates[node.X, node.Y].ListState;
        }

        public bool reachedTarget()
        {
            PathNode node = peakHighestPriorityNode();

            if (node != null && node.X == TargetX && node.Y == TargetY)
            {
                goal = node;
                return true;
            }

            return false;
        }

        public bool isOpenListPopulated()
        {
            if (peakHighestPriorityNode() == null)
            {
                return false;
            }

            return true;
        }

        public float getGScore(PathNode node)
        {
            return CellStates[node.X, node.Y].GScore;
        }

        public void setGScore(PathNode node, float gScore)
        {
            CellStates[node.X, node.Y].GScore = gScore;
        }

        public void adjustNodePriority(PathNode node, float newValue)
        {
            int index = 0;
            for (int i = 0; i < OpenList.Tree.Count; i++)
            {
                if (OpenList.Tree[i].Node.X == node.X && OpenList.Tree[i].Node.Y == node.Y)
                {
                    index = i;
                    break;
                }
            }

            OpenList.adjustPriority(index, newValue);
        }

        public void setNodeParent(PathNode node, PathNode parent)
        {
            CellStates[node.X, node.Y].Parent = parent;
        }

        public PathNode getNodeParent(PathNode node)
        {
            return CellStates[node.X, node.Y].Parent;
        }

        public List<PathNode> getPath()
        {
            List<PathNode> path = getPath(CellStates[TargetX, TargetY].Parent);
            path.Add(goal);
            return path;
        }

        public List<PathNode> getPath(int x, int y)
        {
            return getPath(CellStates[x, y].Parent);
        }

        public List<PathNode> getPath(PathNode node)
        {
            PathNode parent = getNodeParent(node);

            if (parent == null || (parent.X == StartX && parent.Y == StartY))
            {
                return new List<PathNode>();
            }
            else
            {
                List<PathNode> path = getPath(parent);
                path.Add(node);
                return path;
            }
        }

    }
}