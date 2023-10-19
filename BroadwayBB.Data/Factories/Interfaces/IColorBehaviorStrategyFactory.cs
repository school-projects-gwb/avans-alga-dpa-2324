using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Data.Factories.Interfaces;

public interface IColorBehaviorStrategyFactory
{
    public IColorBehaviorStrategy Create(ColorName name);
}