using System.Drawing;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public abstract class PathfinderStrategyBase : IPathfinderStrategy
{
    protected (List<List<ITile>> shortestPaths, List<ITile> visitedNodes) CurrentPath;

    public abstract void CalculatePath(List<TileNode> tileGraph, ITile start, ITile target);

    public List<DebugTile> GetDebugInfo(bool withVisited)
    {
        var result = new List<DebugTile>();

        if (CurrentPath.shortestPaths == null || CurrentPath.visitedNodes == null) return new();

        foreach (var shortestPath in CurrentPath.shortestPaths)
        {
            var firstTile = shortestPath.Last();
            result.Add(new DebugTile()
            {
                ColorName = ColorName.White, IsFill = true,
                PositionInfo = new Rectangle(firstTile.Pos.Xi, firstTile.Pos.Yi, 1, 1)
            });
        }

        foreach (var shortestPath in CurrentPath.shortestPaths)
            foreach (var tile in shortestPath.Take(shortestPath.Count - 1))
                result.Add(new DebugTile()
                {
                    ColorName = ColorName.Black, IsFill = true, PositionInfo = new Rectangle(tile.Pos.Xi, tile.Pos.Yi, 1, 1)
                });

        if (!withVisited) return result;

        foreach (var tile in CurrentPath.visitedNodes)
            result.Add(new DebugTile()
            {
                ColorName = ColorName.Black, IsFill = false,
                PositionInfo = new Rectangle(tile.Pos.Xi, tile.Pos.Yi, 1, 1)
            });

        return result;
    }

    protected void ShowPathLength()
    {
        foreach (var shortestPath in CurrentPath.shortestPaths)
            Console.WriteLine("Padlengte: " + shortestPath.Count);
    } 

    public bool IsTileInPath(ITile targetNodeTile)
    {
        if (CurrentPath.shortestPaths == null) return false;
        
        foreach (var shortestPath in CurrentPath.shortestPaths)
            if (shortestPath.Contains(targetNodeTile)) return true;
        
        return false;
    }
}