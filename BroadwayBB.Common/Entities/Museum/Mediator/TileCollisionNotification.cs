using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Museum.Mediator;

public class TileCollisionNotification : NotificationBase
{
    public TileCollisionResult TileCollisionResult { get; }

    public TileCollisionNotification(TileCollisionResult tileCollisionResult)
    {
        TileCollisionResult = tileCollisionResult;
    }
}