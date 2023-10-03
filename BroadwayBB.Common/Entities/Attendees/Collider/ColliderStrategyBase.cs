using System.Drawing;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public abstract class ColliderStrategyBase : IColliderStrategy
{
    private readonly int _minimumCollisionDistance = 2;
    public Rectangle Size { get; private set; }
    
    public ColliderStrategyBase(Rectangle size) => Size = size;

    public abstract void HandleCollision(List<IAttendee> attendees);

    protected void HandleIsColliding(IAttendee one, IAttendee two)
    {
        double dx = one.Movement.GridPosX - two.Movement.GridPosX;
        double dy = one.Movement.GridPosY - two.Movement.GridPosY;
        var dist = Math.Sqrt(dx * dx + dy * dy);
            
        one.Movement.IsColliding = dist <= _minimumCollisionDistance;
        two.Movement.IsColliding = one.Movement.IsColliding;
    }
    
    public abstract List<Rectangle> GetDebugInfo();
}