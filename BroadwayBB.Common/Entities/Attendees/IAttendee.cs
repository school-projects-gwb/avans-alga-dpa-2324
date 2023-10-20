namespace BroadwayBB.Common.Entities.Attendees;

public interface IAttendee
{
    IMovement Movement { get; }
    public IAttendee Clone();
}