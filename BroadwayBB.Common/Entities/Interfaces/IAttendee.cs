namespace BroadwayBB.Common.Entities.Interfaces;

public interface IAttendee
{
    IMovement Movement { get; }
    public IAttendee DeepCopy();
}