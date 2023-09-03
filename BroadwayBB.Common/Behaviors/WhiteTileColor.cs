using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Behaviors;

public class WhiteTileColor : ITileColorBehavior
{
    public ColorName ColorName { get; } = ColorName.White;
    
    public ICollisionResult HandleCollision()
    {
        return new CollisionResult(this);
    }

    public bool CanMove()
    {
        throw new NotImplementedException();
    }
}