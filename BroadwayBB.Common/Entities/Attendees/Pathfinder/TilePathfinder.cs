using BroadwayBB.Common.Entities.Museum;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public class TilePathfinder : IConfigObserver
{
    private List<TileNode> _tileGraph = new();
    private (List<ITile> shortestPath, List<ITile> visitedNodes) _currentPath;
    private Dictionary<PathfinderStrategyType, IPathfinderStrategy> _strategies = new()
    {
        { PathfinderStrategyType.Bfs, new BfsPathfinderStrategy() },
        { PathfinderStrategyType.Dijkstra, new DijkstraPathfinderStrategy()}
    };
    
    private PathfinderStrategyType _activeStrategyType = PathfinderStrategyType.Bfs;

    public void SetTiles(List<TileNode> tileGraph) => _tileGraph = tileGraph;

    public void GeneratePath(ITile start, ITile target) => _strategies[_activeStrategyType].CalculatePath(_tileGraph, start, target);

    public List<DebugTile> GetDebugInfo() => _strategies[_activeStrategyType].GetDebugInfo();
    
    public void OnUpdate(ConfigType type, bool value)
    {
        
    }
}