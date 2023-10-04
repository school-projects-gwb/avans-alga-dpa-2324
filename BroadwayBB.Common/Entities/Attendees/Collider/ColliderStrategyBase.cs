using System.Drawing;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public abstract class ColliderStrategyBase : IColliderStrategy
{
    private readonly int _minimumCollisionDistance = 1;
    public Rectangle Size { get; private set; }

    protected ColliderStrategyBase(Rectangle size) => Size = size;

    public abstract void HandleCollision(List<IAttendee> attendees);

    protected void HandleIsColliding(IAttendee one, IAttendee two)
    {
        double dx = Math.Abs(one.Movement.GridPos.Xd - two.Movement.GridPos.Xd);
        double dy = Math.Abs(one.Movement.GridPos.Yd - two.Movement.GridPos.Yd);
        
        bool isColliding = dx <= _minimumCollisionDistance && dy <= _minimumCollisionDistance;
        
        one.Movement.IsColliding = isColliding;
        two.Movement.IsColliding = isColliding;
    }
    
    public abstract List<Rectangle> GetDebugInfo();
}