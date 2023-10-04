using System.Drawing;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public abstract class ColliderStrategyBase : IColliderStrategy
{
    private readonly int _minimumCollisionDistance = 1;
    public Rectangle Size { get; private set; }

    protected ColliderStrategyBase(Rectangle size) => Size = size;

    public abstract void HandleCollision(List<IAttendee> attendees);

    protected void HandleIsColliding(IAttendee one, IAttendee two)
    {
        double dx = Math.Abs(one.Movement.GridPosX - two.Movement.GridPosX);
        double dy = Math.Abs(one.Movement.GridPosY - two.Movement.GridPosY);
        
        bool isColliding = dx <= _minimumCollisionDistance && dy <= _minimumCollisionDistance;
        
        one.Movement.IsColliding = isColliding;
        two.Movement.IsColliding = isColliding;
    }
    
    public abstract List<DebugTile> GetDebugInfo();
}