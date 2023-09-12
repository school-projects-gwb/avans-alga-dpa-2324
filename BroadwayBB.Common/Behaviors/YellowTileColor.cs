using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Behaviors;

public class YellowTileColor : ITileColorBehavior
{
    public ColorName ColorName => ColorName.Yellow;
    private TileColorCounter TileColorCounter { get; set; } = new(2);
    
    public ColorBehaviorResult HandleCollision()
    {
        TileColorCounter.Increase();
        if (TileColorCounter.LimitReached())
            return new ColorBehaviorResult{UpdatedCollisionTargetTileColor = new GreyTileColor(), ShouldCreateArtist = true};
        
        return new ColorBehaviorResult{UpdatedCollisionTargetTileColor = this, ShouldCreateArtist = true};
    }
    
    public ITileColorBehavior DeepCopy()
    {
        var colorBehaviorCopy = new YellowTileColor();
        colorBehaviorCopy.TileColorCounter = TileColorCounter.DeepCopy();

        return colorBehaviorCopy;
    }
}