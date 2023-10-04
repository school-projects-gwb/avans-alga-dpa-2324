using System.Drawing;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public interface IColliderStrategy
{
    public void HandleCollision(List<IAttendee> attendees);
    public List<DebugTile> GetDebugInfo();
}