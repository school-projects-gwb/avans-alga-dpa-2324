using System.Drawing;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public abstract class ColliderStrategyBase : IColliderStrategy
{
    public Rectangle Size { get; private set; }
    
    public ColliderStrategyBase(Rectangle size) => Size = size;

    public abstract void HandleCollision(List<IAttendee> attendees);

    protected void HandleIsColliding(IAttendee one, IAttendee two)
    {
        double dx = one.Movement.GridPosX - two.Movement.GridPosX;
        double dy = one.Movement.GridPosY - two.Movement.GridPosY;
        var dist = Math.Sqrt(dx * dx + dy * dy);
            
        two.Movement.IsColliding = dist <= 0.5;
        one.Movement.IsColliding = dist <= 0.5;
    }
    
    public abstract List<Rectangle> GetDebugInfo();
}