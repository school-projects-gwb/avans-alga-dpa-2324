using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Behaviors;

public class WhiteTileColor : ITileColorBehavior
{
    public ColorName ColorName => ColorName.White;

    public ColorBehaviorResult HandleCollision()
    {
        return new ColorBehaviorResult { UpdatedCollisionTargetTileColor = this };
    }
}