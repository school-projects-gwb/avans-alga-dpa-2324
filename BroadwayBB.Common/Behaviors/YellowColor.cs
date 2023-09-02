using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Behaviors;

public class YellowColor : IColorBehavior
{
    private TileColorCounter TileColorCounter { get; }
    public YellowColor() => TileColorCounter = new TileColorCounter(2);
    
    public ICollisionResult HandleCollision()
    {
        throw new NotImplementedException();
    }

    public bool CanMove()
    {
        throw new NotImplementedException();
    }
}