using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Behaviors.Interfaces;

public interface IColorBehavior
{
    public ICollisionResult HandleCollision();
    public bool CanMove();
}