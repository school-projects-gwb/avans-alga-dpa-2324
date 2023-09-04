using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;
using BroadwayBB.Common.Models.Structures;

namespace BroadwayBB.Common.Behaviors;

public class GreyTileColor : ITileColorBehavior
{
    public ColorName ColorName => ColorName.Grey;

    private TileColorCounter TileColorCounter { get; } = new(3);

    public ICollisionResult HandleCollision()
    {
        TileColorCounter.Increase();
        return TileColorCounter.LimitReached() ?
            new ColorBehaviorResult(new RedTileColor()) 
            : new ColorBehaviorResult(this);
    }

    public bool CanMove()
    {
        return true;
    }
}