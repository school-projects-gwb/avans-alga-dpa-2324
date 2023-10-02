using BroadwayBB.Common.Entities.Attendees;

namespace BroadwayBB.Data.Factories.Interfaces;

public interface IAttendeeFactory
{
    public IAttendee Create(double posX, double posY, double speedVertical, double speedHorizontal);
}