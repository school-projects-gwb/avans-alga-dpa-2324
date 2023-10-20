using BroadwayBB.Common.Entities.Attendees;

namespace BroadwayBB.Common.Entities.Museum.Mediator;

public class AttendeeMovementNotification : NotificationBase
{
    public IAttendee Attendee { get; }
    public List<MovementDirection> PossibleDirections { get; }
    
    public AttendeeMovementNotification(IAttendee attendee, List<MovementDirection> possibleDirections)
    {
        Attendee = attendee;
        PossibleDirections = possibleDirections;
    }
}