using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models;

public class Tile : ITile
{
    public int PosX { get; }
    public int PosY { get; }
    public IColorBehavior ColorBehavior { get; }

    public Tile(int posX, int posY, IColorBehavior colorBehavior)
    {
        PosX = posX;
        PosY = posY;
        ColorBehavior = colorBehavior;
    }
    
    public ICollisionResult HandleCollision(IAttendee attendee) => ColorBehavior.HandleCollision();
    
    public bool CanMove(IAttendee attendee) => ColorBehavior.CanMove();
}