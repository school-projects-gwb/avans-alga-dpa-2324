namespace BroadwayBB.Common.Entities.Tiles;

public class TileNode
{
    public ITile Tile { get; }

    public List<TileNode> Neighbors { get; } = new();

    public TileNode(ITile tile) => Tile = tile;
}