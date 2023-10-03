using System.Drawing;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public interface IColliderStrategy
{
    public void HandleCollision(List<IAttendee> attendees);
    public List<Rectangle> GetDebugInfo();
}