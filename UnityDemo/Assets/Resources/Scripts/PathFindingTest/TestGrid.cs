using PathFinding.General;
using PathFinding.GridStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class TestGrid : IGrid
    {

        public int Width { get; set; }
        public int Height { get; set; }

        public ExtendedPathNode[,] Grid;

        public TestGrid(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new ExtendedPathNode[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Grid[i, j] = new ExtendedPathNode(i, j);
                }
            }
        }

        public void setObstacle(int x, int y)
        {
            Grid[x, y].Walkable = false;
        }

		public void removeObstacle(int x, int y)
		{
			Grid[x, y].Walkable = true;
		}

        public float getMovementCost(PathNode node1, PathNode node2)
        {
			float cost = 1;
			if (node1.X - node2.X != 0 && node1.Y != node2.Y) {
				cost = 1.5f;
			}
            return cost;
        }

        public List<PathNode> getNeighbours(PathNode node)
        {
            List<PathNode> neighbours = new List<PathNode>();

			for (int i = node.X - 1; i <= node.X + 1; i ++) {
				for (int j = node.Y - 1; j <= node.Y + 1; j ++) {
					if ((j != node.Y || i != node.X) && j < Height && j >= 0 && i < Width && i >= 0 && Grid[i, j].Walkable)
		            {
						bool addNode = false;
						if (j==node.Y + 1 && i == node.X + 1){
							if (Grid[i, j-1].Walkable && Grid[i-1, j].Walkable){
								addNode = true;
							}
						}
						else if (j==node.Y - 1 && i == node.X + 1){
							if (Grid[i, j+1].Walkable && Grid[i-1, j].Walkable){
								addNode = true;
							}
						}
						else if (j==node.Y + 1 && i == node.X - 1){
							if (Grid[i, j-1].Walkable && Grid[i+1, j].Walkable){
								addNode = true;
							}
						}
						else if (j==node.Y - 1 && i == node.X - 1){
							if (Grid[i, j+1].Walkable && Grid[i+1, j].Walkable){
								addNode = true;
							}
						}
						else{
							addNode = true;
						}
						if (addNode){
		                	neighbours.Add(Grid[i,j]);
						}
		            }
				}
			}

            return neighbours;
        }

        public String[,] getGridAsArray()
        {
            String[,] gridString = new String[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (Grid[i, j].Walkable)
                    {
                        gridString[i, j] = "-";
                    }
                    else
                    {
                        gridString[i, j] = "X";
                    }
                }
            }
            return gridString;
        }
    }
}