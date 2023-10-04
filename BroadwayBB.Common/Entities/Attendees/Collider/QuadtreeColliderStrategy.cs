using System.Drawing;
using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Quadtree;
using BroadwayBB.Common.Entities.Tiles;

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
                attendee.Movement.GetRoundedGridPosX(), 
                attendee.Movement.GetRoundedGridPosY()
            );
        }
        
        attendees.ForEach(attendee => HandleAttendeeCollision(attendee));
    }

    private void HandleAttendeeCollision(IAttendee attendee)
    {
        var targetTreeObject = new TreeObject<IAttendee>(attendee, attendee.Movement.GetRoundedGridPosX(),
            attendee.Movement.GetRoundedGridPosY());

        List<IAttendee> result = new();

        _attendeeQuadtree.GetObjectsInQuadrant(result, targetTreeObject);
        Console.WriteLine(result.Count);
        foreach (var obj in result) HandleIsColliding(obj, attendee);
    }

    public override List<DebugTile> GetDebugInfo() => _attendeeQuadtree.GetNodeCoordinates();
}