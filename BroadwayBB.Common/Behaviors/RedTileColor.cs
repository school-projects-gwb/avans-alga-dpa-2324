using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Behaviors;

public class RedTileColor : ITileColorBehavior
{
    public ColorName ColorName => ColorName.Red;

    public ICollisionResult HandleCollision()
    {
        throw new NotImplementedException();
    }

    public bool CanMove()
    {
        return true;
    }
}