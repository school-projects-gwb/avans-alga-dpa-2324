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
        _pathsChanged = true;
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

            if (node.Tile.Tile == end)
            {
                if (start == node.Tile.Tile) return;
                shortestPaths = GetShortestPaths(node);
                break;
            }

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
        
        Console.WriteLine("---Dijkstra---");
        ShowPathWeight();
    }

    private List<List<TileNodeWeightedDecorator>> GetShortestPaths(TileNodeWeightedDecorator node)
    {
        var paths = new List<List<TileNodeWeightedDecorator>>();
        var initialPath = new List<TileNodeWeightedDecorator> { node };
        var queue = new Queue<List<TileNodeWeightedDecorator>>();
        queue.Enqueue(initialPath);

        while (queue.Count > 0)
        {
            var currentPath = queue.Dequeue();
            var currentNode = currentPath.Last();

            if (currentNode.Weight.Key == null)
            {
                paths.Add(currentPath);
                continue;
            }

            var lowestWeight = currentNode.Neighbors.Min(neighbor => neighbor.Decorator.Weight.Value);
            var neighborsWithLowestWeight = currentNode.Neighbors
                .Where(neighbor => neighbor.Decorator.Weight.Value == lowestWeight)
                .Select(neighbor => neighbor.Decorator);

            foreach (var neighbor in neighborsWithLowestWeight)
            {
                var newPath = new List<TileNodeWeightedDecorator>(currentPath);
                newPath.Add(neighbor);
                queue.Enqueue(newPath);
            }
        }

        return paths;
    }

    private List<List<ITile>> TransformPaths(List<List<TileNodeWeightedDecorator>> shortestPaths)
    {
        return shortestPaths.Select(path => path.Select(p => p.Tile.Tile).ToList()).ToList();
    }
}