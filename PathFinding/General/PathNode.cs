using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinding.General
{
    //General node in a path finding grid. Can be extended if necessary
    public class PathNode
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PathNode(int x, int y)
        {
            X = x;
            Y = y;
        }

        public String generateID()
        {
            return X + "|" + Y;
        }
    }
}