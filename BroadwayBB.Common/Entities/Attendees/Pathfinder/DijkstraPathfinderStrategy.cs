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
        var unvisited = new List<TileNodeWeightedDecorator> { startNode };
        var visitedNodes = new List<ITile>();

        while (unvisited.Any())
        {
            var node = unvisited.OrderBy(node1 => node1.Weight.Value).First();
            visitedNodes.Add(node.Tile.Tile);
            
            foreach (var neighbour in node.Neighbors)
            {
                if (!(neighbour.Weight + node.Weight.Value < neighbour.Decorator.Weight.Value)) continue;
                neighbour.Decorator.Weight = new(node.Tile, neighbour.Weight + node.Weight.Value);

                if (!unvisited.Contains(neighbour.Decorator) && !neighbour.Decorator.Visited) unvisited.Add(neighbour.Decorator);
            }
            
            node.Visited = true;
            unvisited.Remove(node);
        }
        
        var endNode = graph.Nodes.First(node => node.Tile.Tile == end);
        var path = new List<TileNodeWeightedDecorator> { endNode };
        
        while (endNode.Weight.Key != null)
        {
            endNode = endNode.Neighbors.OrderBy(edge => edge.Decorator.Weight.Value).FirstOrDefault()?.Decorator;
            if (endNode == null) return;
            path.Add(endNode);
        }
        
        CurrentPath = (path.Select(p => p.Tile.Tile).ToList(), visitedNodes)!;
    }
}