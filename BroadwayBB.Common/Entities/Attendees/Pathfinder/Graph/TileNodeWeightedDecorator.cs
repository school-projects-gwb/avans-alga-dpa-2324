using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder.Graph;

public class TileNodeWeightedDecorator
{
    public readonly TileNode Tile;
    public KeyValuePair<TileNode?, double> Weight { get; set; } = new(null, int.MaxValue);
    public List<WeightedEdge> Neighbors { get; } = new();
    public bool Visited { get; set; }

    public TileNodeWeightedDecorator(TileNode tile) => Tile = tile;
}