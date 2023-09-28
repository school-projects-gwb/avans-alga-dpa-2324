using System.Xml;
using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Common.Entities;

public class TileNode
{
    public ITile Tile { get; }

    public List<TileNode> Neighbors { get; } = new();

    public TileNode(ITile tile)
    {
        Tile = tile;
    }

    public TileNode DeepCopy()
    {
        return new TileNode(Tile.DeepCopy());
    }
}