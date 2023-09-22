using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Behaviors;

public class WhiteColorBehaviorStrategy : IColorBehaviorStrategy
{
    public ColorName ColorName => ColorName.White;

    public ColorBehaviorResult HandleCollision()
    {
        return new ColorBehaviorResult { UpdatedCollisionTargetColor = this };
    }
    
    public IColorBehaviorStrategy DeepCopy() => new WhiteColorBehaviorStrategy();
}