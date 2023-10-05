using BroadwayBB.Common.Entities.Museum;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public class TilePathfinder : IConfigObserver
{
    private List<TileNode> _tileGraph = new();
    
    private Dictionary<PathfinderStrategyType, IPathfinderStrategy> _strategies = new()
    {
        { PathfinderStrategyType.Bfs, new BfsPathfinderStrategy() },
        { PathfinderStrategyType.Dijkstra, new DijkstraPathfinderStrategy()}
    };
    
    private PathfinderStrategyType _activeStrategyType = PathfinderStrategyType.Bfs;

    public void SetTiles(List<TileNode> tileGraph) => _tileGraph = tileGraph;

    public void GeneratePath(ITile start, ITile target) => _strategies[_activeStrategyType].CalculatePath(_tileGraph, start, target);

    public List<DebugTile> GetDebugInfo(bool withVisited) => _strategies[_activeStrategyType].GetDebugInfo(withVisited);
    
    public void OnUpdate(ConfigType type, bool value)
    {
        if (type == ConfigType.IsBfsPathfinding) SetNextStrategy();
    }
    
    private void SetNextStrategy()
    {
        if (!_strategies.ContainsKey(_activeStrategyType))
        {
            _activeStrategyType = _strategies.Keys.First();
            return;
        }
    
        var currentIndex = Array.IndexOf(_strategies.Keys.ToArray(), _activeStrategyType);
        var nextIndex = (currentIndex + 1) % _strategies.Count;
        _activeStrategyType = _strategies.Keys.ToArray()[nextIndex];
    }

    public bool IsTileInPath(TileNode targetNode) => _strategies[_activeStrategyType].IsTileInPath(targetNode.Tile);
}