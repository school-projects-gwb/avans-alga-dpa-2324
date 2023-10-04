using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Tiles;

public class Tile : ITile
{
    public Coords Pos { get; }
    public IColorBehaviorStrategy ColorBehaviorStrategy { get; private set; }

    public Tile(Coords coords, IColorBehaviorStrategy colorBehaviorStrategy)
    {
        Pos = coords;
        ColorBehaviorStrategy = colorBehaviorStrategy;
    }

    public void UpdateColorBehavior(IColorBehaviorStrategy newBehaviorStrategy) => ColorBehaviorStrategy = newBehaviorStrategy;
    
    public ITile DeepCopy() => new Tile(Pos, ColorBehaviorStrategy.DeepCopy());
}