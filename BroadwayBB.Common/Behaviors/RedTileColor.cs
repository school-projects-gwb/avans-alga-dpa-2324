using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Behaviors;

public class RedTileColor : ITileColorBehavior
{
    public ColorName ColorName => ColorName.Red;

    public ColorBehaviorResult HandleCollision()
    {
        return new ColorBehaviorResult{UpdatedCollisionTargetTileColor = new BlueTileColor(), ShouldRemoveArtist = true};
    }
    
    public ITileColorBehavior DeepCopy() => new RedTileColor();
}