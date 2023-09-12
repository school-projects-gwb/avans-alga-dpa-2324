using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Behaviors.Interfaces;

public interface ITileColorBehavior
{
    public ColorName ColorName { get; }
    public ColorBehaviorResult HandleCollision();
    public ITileColorBehavior DeepCopy();
}