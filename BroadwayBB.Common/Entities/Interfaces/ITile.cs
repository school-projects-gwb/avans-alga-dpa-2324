using BroadwayBB.Common.Behaviors.Interfaces;

namespace BroadwayBB.Common.Entities.Interfaces;

public interface ITile
{
    public int PosX { get; }
    public int PosY { get; }
    
    public ITileColorBehavior TileColorBehavior { get; }

    public void UpdateColorBehavior(ITileColorBehavior newBehavior);
    
    public bool CanMove(IAttendee attendee)
    {
        throw new NotImplementedException();
    }
}