using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Behaviors;

public class GreyTileColor : ITileColorBehavior
{
    public ColorName ColorName { get; } = ColorName.Grey;
    
    private TileColorCounter TileColorCounter { get; } = new(3);

    public ICollisionResult HandleCollision()
    {
        TileColorCounter.Increase();
        return TileColorCounter.LimitReached() ?
            new CollisionResult(new RedTileColor()) 
            : new CollisionResult(this);
    }

    public bool CanMove()
    {
        throw new NotImplementedException();
    }
}