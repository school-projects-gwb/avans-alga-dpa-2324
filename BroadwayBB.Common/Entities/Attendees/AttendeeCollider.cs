using System.Drawing;
using BroadwayBB.Common.Entities.Extensions;

namespace BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Quadtree;

public class AttendeeCollider
{
    private List<IAttendee> _attendees = new();
    private Quadtree<IAttendee> _attendeeQuadtree;

    public AttendeeCollider(Rectangle simulationSize) => _attendeeQuadtree = new Quadtree<IAttendee>(0, simulationSize);

    public void SetAttendees(List<IAttendee> attendees) => _attendees = attendees;
    
    public void ResetCollider()
    {
        
    }
    
    public void HandleCollision()
    {
        _attendeeQuadtree.Clear();
        
        foreach (var attendee in _attendees)
        {
            _attendeeQuadtree.Insert(
                attendee, 
                attendee.Movement.GetRoundedGridPosX(), 
                attendee.Movement.GetRoundedGridPosY()
            );
        }
        
        _attendees.ForEach(attendee => HandleAttendeeCollision(attendee));
    }

    private void HandleAttendeeCollision(IAttendee attendee)
    {
        var targetTreeObject = new TreeObject<IAttendee>(attendee, attendee.Movement.GetRoundedGridPosX(),
            attendee.Movement.GetRoundedGridPosY());

        List<IAttendee> result = new();

        _attendeeQuadtree.GetObjectsInQuadrant(result, targetTreeObject);
        
        foreach (var obj in result)
        {
            double dx = obj.Movement.GridPosX - attendee.Movement.GridPosX;
            double dy = obj.Movement.GridPosY - attendee.Movement.GridPosY;
            var dist = Math.Sqrt(dx * dx + dy * dy);
            
            attendee.Movement.IsColliding = dist <= 0.5;
            obj.Movement.IsColliding = dist <= 0.5;
        }
    }

    public List<Rectangle> GetDebugInfo()
    {
        return _attendeeQuadtree.GetNodeCoordinates();
    }
    
    public void SetStrategy()
    {
        
    }
}