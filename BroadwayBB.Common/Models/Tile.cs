using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;
using BroadwayBB.Common.Models.Structures;

namespace BroadwayBB.Common.Models;

public class Tile : ITile
{
    public int PosX { get; }
    public int PosY { get; }
    public ITileColorBehavior TileColorBehavior { get; }

    public Tile(int posX, int posY, ITileColorBehavior tileColorBehavior)
    {
        PosX = posX;
        PosY = posY;
        TileColorBehavior = tileColorBehavior;
    }

    public ColorBehaviorResult HandleCollision(IAttendee attendee)
    {
        var result = TileColorBehavior.HandleCollision();
        return result;
    } 
    
    public bool CanMove(IAttendee attendee) => TileColorBehavior.CanMove();
}