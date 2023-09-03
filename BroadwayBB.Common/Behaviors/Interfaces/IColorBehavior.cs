using BroadwayBB.Common.Enums;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Behaviors.Interfaces;

public interface IColorBehavior
{
    public ColorName ColorName { get; }
    public ICollisionResult HandleCollision();
    public bool CanMove();
}