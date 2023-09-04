using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Common.Entities;

public class Artist : IAttendee
{
    public IMovement Movement { get; }
    public Artist(double posX, double posY, double speedVertical, double speedHorizontal)
    {
        Movement = new Movement(posX, posY, speedVertical, speedHorizontal, MovementDirectionExtensions.GetRandomDirection());
    }
}