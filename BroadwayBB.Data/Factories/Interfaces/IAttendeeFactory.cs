using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Data.Factories.Interfaces;

public interface IAttendeeFactory
{
    public IAttendee Create(Coords coords, double speedVertical, double speedHorizontal);
}