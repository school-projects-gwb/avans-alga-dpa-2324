using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities;

public class Tile : ITile
{
    public int PosX { get; }
    public int PosY { get; }
    public ITileColorBehavior TileColorBehavior { get; private set; }

    public Tile(int posX, int posY, ITileColorBehavior tileColorBehavior)
    {
        PosX = posX;
        PosY = posY;
        TileColorBehavior = tileColorBehavior;
    }
    
    public void UpdateColorBehavior(ITileColorBehavior newBehavior) => TileColorBehavior = newBehavior;
    
    public ITile DeepCopy() => new Tile(PosX, PosY, TileColorBehavior.DeepCopy());
}