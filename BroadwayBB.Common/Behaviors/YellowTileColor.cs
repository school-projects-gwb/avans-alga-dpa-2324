using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;
using BroadwayBB.Common.Models.Structures;

namespace BroadwayBB.Common.Behaviors;

public class YellowTileColor : ITileColorBehavior
{
    public ColorName ColorName => ColorName.Yellow;
    private TileColorCounter TileColorCounter { get; } = new(2);
    
    public ColorBehaviorResult HandleCollision()
    {
        TileColorCounter.Increase();
        if (TileColorCounter.LimitReached())
            return new ColorBehaviorResult{UpdatedCollisionTargetTileColor = this, ShouldCreateArtist = true};
        
        return new ColorBehaviorResult{UpdatedCollisionTargetTileColor = new GreyTileColor(), ShouldCreateArtist = true};
    }

    public bool CanMove() => true;
}