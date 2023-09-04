using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;
using BroadwayBB.Common.Models.Structures;

namespace BroadwayBB.Common.Behaviors;

public class WhiteTileColor : ITileColorBehavior
{
    public ColorName ColorName => ColorName.White;

    public ICollisionResult HandleCollision()
    {
        return new ColorBehaviorResult(this);
    }

    public bool CanMove()
    {
        return true;
    }
}