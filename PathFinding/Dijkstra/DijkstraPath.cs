using System.Collections.Generic;
using PathFinding.General;
using PathFinding.Collections;
namespace PathFinding.Dijkstra
{

    //Maintains the state of the path search from frame to frame. Allows for time splicing. Most of the class 
    //is just the implementation of the functions used in the Dijkstra Algo
    public class DijkstraPath : PathContainer
    {

        public int StartX, StartY;
        public BinaryHeap<PathNode> OpenList { get; set; }

        public CellState[,] CellStates;

        public bool Complete { get; set; }

        private ILocationTest locationTest;

        private PathNode goal;

        public DijkstraPath(int startX, int startY, int gridWidth, int gridHeight, ILocationTest locationTest)
        {
            Complete = false;
            StartX = startX;
            StartY = startY;
            this.locationTest = locationTest;

            //TODO: What is this? Where did I take this from and why is it here?
            //dist[source] := 0                     // Initializations
            //for each vertex v in Graph:           
            //    if v ≠ source
            //	      dist[v] := infinity           // Unknown distance from source to v
            //		  previous[v] := undefined      // Predecessor of v
            //	  end if
            //	  Q.add_with_priority(v,dist[v])
            //end for

            OpenList = new BinaryHeap<PathNode>();
            CellStates = new CellState[gridWidth, gridHeight];
            //Initialise the CellStates for the search
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    CellStates[i, j] = new CellState();
                    if (i != StartX || j != StartY)
                    {
                        PathNode pNode = new PathNode(i, j);
                        addToOpenList(pNode, float.MaxValue);
                        CellStates[i, j].GScore = float.MaxValue;
                    }
                }
            }
            PathNode sourceNode = new PathNode(startX, startY);
            addToOpenList(sourceNode, 0);
            setGScore(sourceNode, 0);
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

        public bool reachedTarget()
        {
            PathNode node = peakHighestPriorityNode();

            if (node != null && getNodeParent(node) != null)
            {
                bool reached = locationTest.testLocation(node.X, node.Y);
                if (reached)
                {
                    goal = node;
                }
                return reached;
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
            PathNode target = peakHighestPriorityNode();
            List<PathNode> path = getPath(target);
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

            if (parent == null)
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

        public CellStatus currentList(PathNode node)
        {
            return CellStates[node.X, node.Y].ListState;
        }

        public void setCurrentList(PathNode node, CellStatus list)
        {
            CellStates[node.X, node.Y].ListState = list;
        }
    }
}