using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Behaviors.Interfaces;

public interface ITileColorBehavior
{
    public ColorName ColorName { get; }
    public ICollisionResult HandleCollision();
    public bool CanMove();
}