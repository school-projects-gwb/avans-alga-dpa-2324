using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Behaviors;

public class YellowColorBehaviorStrategy : IColorBehaviorStrategy
{
    public ColorName ColorName => ColorName.Yellow;
    private ColorBehaviorStrategyCounter ColorBehaviorStrategyCounter { get; set; } = new(2);
    
    public ColorBehaviorResult HandleCollision()
    {
        ColorBehaviorStrategyCounter.Increase();
        if (ColorBehaviorStrategyCounter.LimitReached())
            return new ColorBehaviorResult{UpdatedCollisionTargetColor = new GreyColorBehaviorStrategy(), ShouldCreateArtist = true};
        
        return new ColorBehaviorResult{UpdatedCollisionTargetColor = this, ShouldCreateArtist = true};
    }
    
    public IColorBehaviorStrategy Clone()
    {
        var colorBehaviorCopy = new YellowColorBehaviorStrategy();
        colorBehaviorCopy.ColorBehaviorStrategyCounter = ColorBehaviorStrategyCounter.DeepCopy();

        return colorBehaviorCopy;
    }
}