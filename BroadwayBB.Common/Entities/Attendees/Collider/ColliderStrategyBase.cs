using System.Drawing;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public abstract class ColliderStrategyBase : IColliderStrategy
{
    public Rectangle Size { get; private set; }
    
    public ColliderStrategyBase(Rectangle size) => Size = size;

    public abstract void HandleCollision(List<IAttendee> attendees);

    public abstract List<Rectangle> GetDebugInfo();
}