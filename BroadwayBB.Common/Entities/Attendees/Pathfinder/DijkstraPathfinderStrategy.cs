using System.Drawing;
using BroadwayBB.Common.Entities.Tiles;
using BroadwayBB.Common.Helpers;
using System.Collections.Generic;
using BroadwayBB.Common.Entities.Attendees.PathFinder.Graph;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public class DijkstraPathfinderStrategy : PathfinderStrategyBase
{
    public override void CalculatePath(List<TileNode> tileGraph, ITile start, ITile end)
    {
        var graph = new WeightedGraph(tileGraph);
        var startNode = graph.Nodes.First(node => node.Tile.Tile == start);
        startNode.Weight = new(null, 0);

        var shortestPaths = new List<List<TileNodeWeightedDecorator>>();
        var visitedNodes = new HashSet<ITile>();

        var queue = new Queue<TileNodeWeightedDecorator>();
        queue.Enqueue(startNode);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            visitedNodes.Add(node.Tile.Tile);

            if (node.Tile.Tile == end) AddToShortestPaths(shortestPaths, GetShortestPath(node));

            foreach (var neighbour in node.Neighbors)
            {
                var newDistance = node.Weight.Value + neighbour.Weight;
                if (!(newDistance < neighbour.Decorator.Weight.Value)) continue;
                neighbour.Decorator.Weight = new(node.Tile, newDistance);
                if (!neighbour.Decorator.Visited) queue.Enqueue(neighbour.Decorator);
            }

            node.Visited = true;
        }

        var transformedPaths = TransformPaths(shortestPaths);
        CurrentPath = (transformedPaths, visitedNodes.ToList());
    }

    private List<TileNodeWeightedDecorator> GetShortestPath(TileNodeWeightedDecorator node)
    {
        var path = new List<TileNodeWeightedDecorator> { node };
        while (node.Weight.Key != null)
        {
            node = node.Neighbors.OrderBy(edge => edge.Decorator.Weight.Value).FirstOrDefault()?.Decorator;
            if (node == null) break;
            path.Add(node);
        }
        
        path.Reverse();
        return path;
    }

    private void AddToShortestPaths(List<List<TileNodeWeightedDecorator>> shortestPaths, List<TileNodeWeightedDecorator> path)
    {
        if (shortestPaths.Count == 0 || path.Select(p => p.Weight.Value).Sum() == shortestPaths.First().Select(p => p.Weight.Value).Sum())
            shortestPaths.Add(path.ToList());
    }

    private List<List<ITile>> TransformPaths(List<List<TileNodeWeightedDecorator>> shortestPaths)
    {
        return shortestPaths.Select(path => path.Select(p => p.Tile.Tile).ToList()).ToList();
    }
}