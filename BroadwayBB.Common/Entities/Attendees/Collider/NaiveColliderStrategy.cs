using System.Drawing;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public class NaiveColliderStrategy : ColliderStrategyBase
{
    public NaiveColliderStrategy(Rectangle size) : base(size)
    {
    }

    public override void HandleCollision(List<IAttendee> attendees)
    {
        for (int i = 0; i < attendees.Count; i++)
        {
            for (int j = i + 1; j < attendees.Count; j++)
            {
                IAttendee attendee1 = attendees[i];
                IAttendee attendee2 = attendees[j];

                HandleIsColliding(attendee1, attendee2);
            }
        }
    }

    public override List<Rectangle> GetDebugInfo() => new();
}