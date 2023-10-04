using System.Drawing;
using BroadwayBB.Common.Entities.Tiles;

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
                HandleIsColliding(attendees[i], attendees[j]);
                if (attendees[i].Movement.IsColliding) break;
            }
        }
    }

    public override List<DebugTile> GetDebugInfo() => new();
}