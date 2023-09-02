using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Behaviors;

public class GreyColor : IColorBehavior
{
    private TileColorCounter TileColorCounter { get; }
    public GreyColor() => TileColorCounter = new TileColorCounter(3);
    
    public ICollisionResult HandleCollision()
    {
        throw new NotImplementedException();
    }

    public bool CanMove()
    {
        throw new NotImplementedException();
    }
}