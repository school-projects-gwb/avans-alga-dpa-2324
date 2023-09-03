using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Enums;
using BroadwayBB.Common.Models;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Behaviors;

public class WhiteColor : IColorBehavior
{
    public ColorName ColorName => ColorName.White;

    public ICollisionResult HandleCollision()
    {
        return new CollisionResult(this);
    }

    public bool CanMove()
    {
        throw new NotImplementedException();
    }
}