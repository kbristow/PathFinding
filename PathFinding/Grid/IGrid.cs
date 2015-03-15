using PathFinding.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinding.GridStructure
{
    //Interface describing the form required by a grid to be used in the path finder
    public interface IGrid
    {
        //Width and height refer to number of cells
        int Width { get; set; }
        int Height { get; set; }

        //Need to be able to assess the movement cost from one cell to another(neighbouring in general) cell
        float getMovementCost(PathNode node1, PathNode node2);

        //Find the neighbours of a giving cell
        List<PathNode> getNeighbours(PathNode node);

        //TODO: Probably for printing purposes, I dont remember. Figure out
        String[,] getGridAsArray();
    }
}