using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Behaviors;

public class YellowTileColor : ITileColorBehavior
{
    public ColorName ColorName { get; } = ColorName.Yellow;
    private TileColorCounter TileColorCounter { get; } = new(2);
    
    public ICollisionResult HandleCollision()
    {
        TileColorCounter.Increase();
        return new CollisionResult(this);
    }

    public bool CanMove()
    {
        throw new NotImplementedException();
    }
}