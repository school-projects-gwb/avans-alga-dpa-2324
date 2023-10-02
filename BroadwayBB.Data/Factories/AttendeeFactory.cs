using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Data.Factories.Interfaces;

namespace BroadwayBB.Data.Factories;

public class AttendeeFactory : IAttendeeFactory
{
    public IAttendee Create(double posX, double posY, double speedVertical, double speedHorizontal) => new Artist(posX, posY, speedVertical, speedHorizontal);
}