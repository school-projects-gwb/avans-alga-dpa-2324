using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Tiles;

public interface ITile
{
    public Coords Pos { get; }

    public IColorBehaviorStrategy ColorBehaviorStrategy { get; }

    public void UpdateColorBehavior(IColorBehaviorStrategy newBehaviorStrategy);
    
    public ITile Clone();
}