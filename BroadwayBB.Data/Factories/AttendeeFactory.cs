using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Data.Factories.Interfaces;

namespace BroadwayBB.Data.Factories;

public class AttendeeFactory : IAttendeeFactory
{
    public IAttendee Create(Coords coords, double speedVertical, double speedHorizontal) => new Artist(coords, speedVertical, speedHorizontal);
}