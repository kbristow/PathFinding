using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinding.AStar
{
    //Interface used by the AStar algo to assess the heuristic value of a location given a target
    public interface IHeuristic
    {
        float getHeuristicValue(int startX, int startY, int targetX, int targetY);
    }
}