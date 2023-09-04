using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;
using BroadwayBB.Common.Models.Structures;

namespace BroadwayBB.Common.Behaviors;

public class YellowTileColor : ITileColorBehavior
{
    public ColorName ColorName => ColorName.Yellow;
    private TileColorCounter TileColorCounter { get; } = new(2);
    
    public ICollisionResult HandleCollision()
    {
        TileColorCounter.Increase();
        return new ColorBehaviorResult(this);
    }

    public bool CanMove()
    {
        return true;
    }
}