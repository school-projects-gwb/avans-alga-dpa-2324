using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Behaviors;

public class GreyTileColor : ITileColorBehavior
{
    public ColorName ColorName => ColorName.Grey;

    private TileColorCounter TileColorCounter { get; } = new(3);

    public ColorBehaviorResult HandleCollision()
    {
        TileColorCounter.Increase();
        if (TileColorCounter.LimitReached())
            return new ColorBehaviorResult { UpdatedCollisionTargetTileColor = new RedTileColor()};
        
        return new ColorBehaviorResult { UpdatedCollisionTargetTileColor = this };
    }
}