using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Behaviors;

public class GreyColorBehaviorStrategy : IColorBehaviorStrategy
{
    public ColorName ColorName => ColorName.Grey;

    private ColorBehaviorStrategyCounter ColorBehaviorStrategyCounter { get; set; } = new(3);

    public ColorBehaviorResult HandleCollision()
    {
        ColorBehaviorStrategyCounter.Increase();
        if (ColorBehaviorStrategyCounter.LimitReached())
            return new ColorBehaviorResult { UpdatedCollisionTargetColor = new RedColorBehaviorStrategy()};
        
        return new ColorBehaviorResult { UpdatedCollisionTargetColor = this };
    }
    
    public IColorBehaviorStrategy DeepCopy()
    {
        var colorBehaviorCopy = new GreyColorBehaviorStrategy();
        colorBehaviorCopy.ColorBehaviorStrategyCounter = ColorBehaviorStrategyCounter.DeepCopy();

        return colorBehaviorCopy;
    }
}