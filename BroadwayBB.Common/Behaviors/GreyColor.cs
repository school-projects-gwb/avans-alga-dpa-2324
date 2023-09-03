using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Enums;
using BroadwayBB.Common.Models;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Behaviors;

public class GreyColor : IColorBehavior
{
    public ColorName ColorName => ColorName.Grey;
    
    private TileColorCounter TileColorCounter { get; } = new(3);

    public ICollisionResult HandleCollision()
    {
        TileColorCounter.Increase();
        return TileColorCounter.LimitReached() ?
            new CollisionResult(new RedColor()) 
            : new CollisionResult(this);
    }

    public bool CanMove()
    {
        throw new NotImplementedException();
    }
}