using System;
namespace PathFinding.General
{
    //TODO: what is this for? AStart and Dijkstra?
    public class CellState
    {
        public float GScore { get; set; }
        public CellStatus ListState { get; set; }
        public PathNode Parent { get; set; }

        public CellState()
        {
            GScore = 0;
            ListState = CellStatus.NONE;
            Parent = null;
        }
    }
}