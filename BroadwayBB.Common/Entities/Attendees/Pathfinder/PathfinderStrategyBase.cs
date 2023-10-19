using System.Drawing;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities.Tiles;
using BroadwayBB.Common.Helpers;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public abstract class PathfinderStrategyBase : IPathfinderStrategy
{
    protected bool _pathsChanged = true;
    private List<DebugTile> _debugTiles = new();
    
    protected (List<List<ITile>> shortestPaths, List<ITile> visitedNodes) CurrentPath;

    public abstract void CalculatePath(List<TileNode> tileGraph, ITile start, ITile target);

    public List<DebugTile> GetDebugInfo(bool withVisited)
    {
        if (!_pathsChanged) return _debugTiles;
        _pathsChanged = false;
        
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
            {
                if (result.Any(debugTile =>
                        debugTile.PositionInfo.X == tile.Pos.Xi && debugTile.PositionInfo.Y == tile.Pos.Yi)) continue;
                result.Add(new DebugTile()
                {
                    ColorName = ColorName.Black, IsFill = true, PositionInfo = new Rectangle(tile.Pos.Xi, tile.Pos.Yi, 1, 1)
                });
            }
        
        _debugTiles = result;
        
        if (!withVisited) return _debugTiles;

        foreach (var tile in CurrentPath.visitedNodes)
            result.Add(new DebugTile()
            {
                ColorName = ColorName.Black, IsFill = false,
                PositionInfo = new Rectangle(tile.Pos.Xi, tile.Pos.Yi, 1, 1)
            });
        
        _debugTiles = result;
        return _debugTiles;
    }

    protected void ShowPathWeight()
    {
        Console.WriteLine("Padlengte: " + CurrentPath.shortestPaths.First().Sum(tile => 
            tile.ColorBehaviorStrategy.ColorName == 
            ColorName.White ? 0 : WeightRegistryHelper.GetInstance.GetWeight(tile.ColorBehaviorStrategy.ColorName)));
        
        Console.WriteLine("Aantal paden gevonden: " + CurrentPath.shortestPaths.Count);
    } 

    public bool IsTileInPath(ITile targetNodeTile)
    {
        if (CurrentPath.shortestPaths == null) return false;
        
        foreach (var shortestPath in CurrentPath.shortestPaths)
            if (shortestPath.Contains(targetNodeTile)) return true;
        
        return false;
    }
}