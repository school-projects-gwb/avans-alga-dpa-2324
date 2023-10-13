using BroadwayBB.Common.Helpers;

namespace BroadwayBB.Common.Entities.Tiles;

public class TileNode
{
    public ITile Tile { get; }
    
    public int Weight() => WeightRegistryHelper.GetInstance.GetWeight(Tile.ColorBehaviorStrategy.ColorName);

    public List<TileNode> Neighbors { get; } = new();

    public TileNode(ITile tile) => Tile = tile;
}