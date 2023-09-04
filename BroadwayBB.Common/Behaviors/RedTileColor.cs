using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;
using BroadwayBB.Common.Models.Structures;

namespace BroadwayBB.Common.Behaviors;

public class RedTileColor : ITileColorBehavior
{
    public ColorName ColorName => ColorName.Red;

    public ColorBehaviorResult HandleCollision()
    {
        return new ColorBehaviorResult{UpdatedCollisionTargetTileColor = new BlueTileColor(), ShouldRemoveArtist = true};
    }

    public bool CanMove() => true;
}