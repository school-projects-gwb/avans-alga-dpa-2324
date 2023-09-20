using System.Drawing;
using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Behaviors;

public class BlueColorBehaviorStrategy : IColorBehaviorStrategy
{
    public ColorName ColorName => ColorName.Blue;

    public ColorBehaviorResult HandleCollision()
    {
        return new ColorBehaviorResult
        {
            UpdatedCollisionTargetColor = new YellowColorBehaviorStrategy(), 
            ShouldCreateArtist = true, 
            UpdatedAdjacentTileColors = new List<IColorBehaviorStrategy>{ new BlueColorBehaviorStrategy(), new BlueColorBehaviorStrategy() }
        };
    }

    public IColorBehaviorStrategy DeepCopy() => new BlueColorBehaviorStrategy();
}