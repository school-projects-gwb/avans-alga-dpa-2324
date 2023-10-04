using System.Drawing;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public abstract class PathfinderStrategyBase : IPathfinderStrategy
{
    protected (List<ITile> shortestPath, List<ITile> visitedNodes) CurrentPath;
    
    public abstract void CalculatePath(List<TileNode> tileGraph, ITile start, ITile target);

    public List<DebugTile> GetDebugInfo()
    {
        var result = new List<DebugTile>();
        
        if (CurrentPath.shortestPath == null || CurrentPath.visitedNodes == null) return new();

        var firstTile = CurrentPath.shortestPath.First();
        result.Add(new DebugTile() { ColorName = ColorName.White, IsFill = true, PositionInfo = new Rectangle(firstTile.PosX, firstTile.PosY, 1, 1)});
        
        foreach (var tile in CurrentPath.shortestPath.Skip(1))
            result.Add(new DebugTile() { ColorName = ColorName.Black, IsFill = true, PositionInfo = new Rectangle(tile.PosX, tile.PosY, 1, 1)});
        
        foreach (var tile in CurrentPath.visitedNodes)
            result.Add(new DebugTile() { ColorName = ColorName.Black, IsFill = false, PositionInfo = new Rectangle(tile.PosX, tile.PosY, 1, 1)});

        return result;
    }
}