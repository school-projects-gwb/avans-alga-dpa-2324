using System.Drawing;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public class DijkstraPathfinderStrategy : IPathfinderStrategy
{
    public (List<ITile> shortestPath, List<ITile> visitedNodes) CalculatePath(List<TileNode> tileGraph, ITile start, ITile end)
    {
        throw new NotImplementedException();
    }

    public List<Rectangle> GetDebugInfo()
    {
        throw new NotImplementedException();
    }
}