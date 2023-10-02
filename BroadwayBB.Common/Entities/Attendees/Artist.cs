using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Common.Entities.Attendees;

public class Artist : IAttendee
{
    public IMovement Movement { get; private set; }
    public Artist(double posX, double posY, double speedVertical, double speedHorizontal)
    {
        Movement = new Movement(posX, posY, speedVertical, speedHorizontal, MovementDirectionExtensions.GetRandomDirection());
    }

    public IAttendee DeepCopy()
    {
        var movement = Movement.DeepCopy();
        var artist = new Artist(0,0,0,0);
        artist.Movement = movement;

        return artist;
    }
}