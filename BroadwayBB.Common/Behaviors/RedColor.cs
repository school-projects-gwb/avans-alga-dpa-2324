using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Enums;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Behaviors;

public class RedColor : IColorBehavior
{
    public ColorName ColorName => ColorName.Red;
    
    public ICollisionResult HandleCollision()
    {
        throw new NotImplementedException();
    }

    public bool CanMove()
    {
        throw new NotImplementedException();
    }
}