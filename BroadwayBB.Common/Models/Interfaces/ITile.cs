using BroadwayBB.Common.Behaviors.Interfaces;

namespace BroadwayBB.Common.Models.Interfaces;

public interface ITile
{
    public int PosX { get; }
    public int PosY { get; }
    
    public ITileColorBehavior TileColorBehavior { get; }

    public ICollisionResult HandleCollision(IAttendee attendee)
    {
        throw new NotImplementedException();
    }
    
    public bool CanMove(IAttendee attendee)
    {
        throw new NotImplementedException();
    }
}