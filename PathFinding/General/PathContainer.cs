using System.Collections.Generic;
namespace PathFinding.General
{
    //Interface that describes the details of a path search. Used by the dijkstra and astar path finders
    public interface PathContainer
    {
        bool Complete { get; set; }
        List<PathNode> getPath();
        List<PathNode> getPath(int x, int y);
        List<PathNode> getPath(PathNode node);
    }
}