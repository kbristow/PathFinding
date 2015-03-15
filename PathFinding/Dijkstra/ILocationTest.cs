using System.Collections;

namespace PathFinding.Dijkstra
{

    //Interface that is used to test if a given location is of the type of the target Dijkstra is looking for
    public interface ILocationTest
    {
        bool testLocation(int x, int y);
    }
}