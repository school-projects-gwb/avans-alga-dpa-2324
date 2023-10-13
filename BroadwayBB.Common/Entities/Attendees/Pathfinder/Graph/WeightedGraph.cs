using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder.Graph;

public class WeightedGraph
{
    public List<TileNodeWeightedDecorator> Nodes { get; }

    public WeightedGraph(List<TileNode> tiles)
    {
        Nodes = tiles.Select(tile => new TileNodeWeightedDecorator(tile)).ToList();

        foreach (var node in Nodes)
            foreach (var neighbor in node.Tile.Neighbors)
            {
                var neighborNode = Nodes.First(node1 => node1.Tile == neighbor);
                node.Neighbors.Add(new (neighborNode, neighbor.Weight()));
            }
    }
}