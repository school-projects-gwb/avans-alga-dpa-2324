using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Common.Entities.Tiles;

public class Tile : ITile
{
    public int PosX { get; }
    public int PosY { get; }
    public IColorBehaviorStrategy ColorBehaviorStrategy { get; private set; }

    public Tile(int posX, int posY, IColorBehaviorStrategy colorBehaviorStrategy)
    {
        PosX = posX;
        PosY = posY;
        ColorBehaviorStrategy = colorBehaviorStrategy;
    }
    
    public void UpdateColorBehavior(IColorBehaviorStrategy newBehaviorStrategy) => ColorBehaviorStrategy = newBehaviorStrategy;
    
    public ITile DeepCopy() => new Tile(PosX, PosY, ColorBehaviorStrategy.DeepCopy());
}