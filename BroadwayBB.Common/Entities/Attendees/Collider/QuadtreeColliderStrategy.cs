using System.Drawing;
using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Quadtree;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public class QuadtreeColliderStrategy : ColliderStrategyBase
{
    private readonly Quadtree<IAttendee> _attendeeQuadtree;

    public QuadtreeColliderStrategy(Rectangle size) : base(size)
    {
        _attendeeQuadtree = new Quadtree<IAttendee>(0, size);
    }

    public override void HandleCollision(List<IAttendee> attendees)
    {
        _attendeeQuadtree.Clear();
        
        foreach (var attendee in attendees)
        {
            _attendeeQuadtree.Insert(
                attendee, 
                attendee.Movement.GridPos.Xi, 
                attendee.Movement.GridPos.Yi
            );
        }
        
        attendees.ForEach(attendee => HandleAttendeeCollision(attendee));
    }

    private void HandleAttendeeCollision(IAttendee attendee)
    {
        var targetTreeObject = new TreeObject<IAttendee>(attendee, attendee.Movement.GridPos.Xi,
            attendee.Movement.GridPos.Yi);

        List<IAttendee> result = new();

        _attendeeQuadtree.GetObjectsInQuadrant(result, targetTreeObject);
        foreach (var obj in result) HandleIsColliding(obj, attendee);
    }

    public override List<Rectangle> GetDebugInfo() => _attendeeQuadtree.GetNodeCoordinates();
}