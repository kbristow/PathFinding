using PathFinding.AStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class TestHeuristic:IHeuristic
    {
        public float getHeuristicValue(int startX, int startY, int targetX, int targetY)
        {
            return Math.Abs(startX - targetX) + Math.Abs(startY-targetY);
        }
    }
}