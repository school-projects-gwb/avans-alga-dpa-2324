using BroadwayBB.Common.Entities.Attendees;

namespace BroadwayBB.Common.Entities.Extensions;

public static class MovementDirectionExtensions
{
    private static readonly Random Random = new();

    public static MovementDirection GetRandomDirection()
    {
        Array values = Enum.GetValues(typeof(MovementDirection));
        return (MovementDirection)(values.GetValue(Random.Next(values.Length)) ?? MovementDirection.North);
    }

    public static MovementDirection GetDirectionBasedOnSpeed(double speedX, double speedY)
    {
        if (speedX != 0) return MovementDirection.East;
        else return MovementDirection.North;
    }
}