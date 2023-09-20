using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Behaviors.Interfaces;

public interface IColorBehaviorStrategy
{
    public ColorName ColorName { get; }
    public ColorBehaviorResult HandleCollision();
    public IColorBehaviorStrategy DeepCopy();
}