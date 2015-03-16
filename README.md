# PathFinding
A C# Pathfinding library built to try and be general enough for most 2D based path finding requirements. The goal is to build a solution that can handle large amounts of entities moving around a map at once. The implementation is built around time splicing the Dijkstra (used for what I have called undirected paths) and A* (used for what I have called directed paths) algorithms.


Included is a Unity Demo where an implementation of the library is shown. Hopefully this demo will evolve to include concepts of path finding such as local obstacle avoidance, path smoothing, group movement etc. The entire project is still a work in progress and only the core time-spliced path finding is implemented. More will be added as time and inspiration allow.
