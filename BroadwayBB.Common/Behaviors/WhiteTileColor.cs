using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;
using BroadwayBB.Common.Models.Structures;

namespace BroadwayBB.Common.Behaviors;

public class WhiteTileColor : ITileColorBehavior
{
    public ColorName ColorName => ColorName.White;

    public ColorBehaviorResult HandleCollision()
    {
        return new ColorBehaviorResult { UpdatedCollisionTargetTileColor = this };
    }

    public bool CanMove() => true;
}