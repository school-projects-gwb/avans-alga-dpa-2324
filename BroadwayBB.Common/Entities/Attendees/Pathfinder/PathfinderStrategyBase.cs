using System.Drawing;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities.Tiles;
using BroadwayBB.Common.Helpers;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public abstract class PathfinderStrategyBase : IPathfinderStrategy
{
    protected readonly int PathHardLimit = 100, PathAmountLimit = 5;
    protected int PathCount = 0;
    protected bool PathsChanged = true;
    private List<DebugTile> _debugTiles = new();
    
    protected (List<List<ITile>> shortestPaths, List<ITile> visitedNodes) CurrentPath = new(new (), new());

    public abstract void CalculatePath(List<TileNode> tileGraph, ITile start, ITile target);

    public List<DebugTile> GetDebugInfo(bool withVisited)
    {
        if (!PathsChanged) return _debugTiles;
        PathsChanged = false;
        
        var result = new List<DebugTile>();

        if (CurrentPath.visitedNodes.Count == 0 || CurrentPath.shortestPaths.Count == 0) return new();

        var firstPath = CurrentPath.shortestPaths.First();
        var firstTile = firstPath.Last();
        result.Add(new DebugTile()
        {
            ColorName = ColorName.White, IsFill = true,
            PositionInfo = new Rectangle(firstTile.Pos.Xi, firstTile.Pos.Yi, 1, 1)
        });

        foreach (var shortestPath in CurrentPath.shortestPaths.Take(100))
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
        if (CurrentPath.shortestPaths.Count > 0)
            Console.WriteLine("Padlengte: " + CurrentPath.shortestPaths.First().Sum(tile => 
                tile.ColorBehaviorStrategy.ColorName == 
                ColorName.White ? 0 : WeightRegistryHelper.GetInstance.GetWeight(tile.ColorBehaviorStrategy.ColorName)));
        
        Console.WriteLine("Aantal paden gevonden: " + PathCount + (PathCount > PathHardLimit ? "+" : ""));
    } 

    public bool IsTileInPath(ITile targetNodeTile)
    {
        if (CurrentPath.shortestPaths == null) return false;
        
        foreach (var shortestPath in CurrentPath.shortestPaths)
            if (shortestPath.Contains(targetNodeTile)) return true;
        
        return false;
    }
}