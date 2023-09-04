using BroadwayBB.Common.Models.Interfaces;
using BroadwayBB.Common.Models.Structures;

namespace BroadwayBB.Common.Behaviors.Interfaces;

public interface ITileColorBehavior
{
    public ColorName ColorName { get; }
    public ColorBehaviorResult HandleCollision();
    public bool CanMove();
}