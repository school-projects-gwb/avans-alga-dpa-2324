using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Attendees;

public class Artist : IAttendee
{
    public IMovement Movement { get; private set; }
    public Artist(Coords coords, double speedVertical, double speedHorizontal)
    {
        Movement = new Movement(coords, speedVertical, speedHorizontal, MovementDirectionExtensions.GetDirectionBasedOnSpeed(speedHorizontal, speedVertical));
    }
    public Artist(Coords coords, double speedVertical, double speedHorizontal, MovementDirection md)
    {
        Movement = new Movement(coords, speedVertical, speedHorizontal, md);
    }

    private Artist(IMovement movement)
    {
        Movement = movement;
    }

    public IAttendee DeepCopy()
    {
        var movement = Movement.DeepCopy();
        var artist = new Artist(movement);

        return artist;
    }
}