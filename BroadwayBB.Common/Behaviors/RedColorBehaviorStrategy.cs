using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Behaviors;

public class RedColorBehaviorStrategy : IColorBehaviorStrategy
{
    public ColorName ColorName => ColorName.Red;

    public ColorBehaviorResult HandleCollision()
    {
        return new ColorBehaviorResult{UpdatedCollisionTargetColor = new BlueColorBehaviorStrategy(), ShouldRemoveArtist = true};
    }
    
    public IColorBehaviorStrategy DeepCopy() => new RedColorBehaviorStrategy();
}