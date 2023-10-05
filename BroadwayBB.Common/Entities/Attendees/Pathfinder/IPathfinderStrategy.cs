using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public interface IPathfinderStrategy
{
    public void CalculatePath(List<TileNode> tileGraph, ITile start, ITile target);
    public List<DebugTile> GetDebugInfo(bool withVisited);

    bool IsTileInPath(ITile targetNodeTile);
}