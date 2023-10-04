using System.Drawing;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public interface IPathfinderStrategy
{
    public (List<ITile> shortestPath, List<ITile> visitedNodes) CalculatePath(List<TileNode> tileGraph, ITile start, ITile target);
    
    public List<Rectangle> GetDebugInfo();
}