namespace BroadwayBB.Common.Models;

public enum MovementDirection
{
    North, East, South, West
}

public static class MovementDirectionExtensions
{
    private static readonly Random Random = new Random();

    public static MovementDirection GetRandomDirection()
    {
        Array values = Enum.GetValues(typeof(MovementDirection));
        return (MovementDirection)(values.GetValue(Random.Next(values.Length)) ?? MovementDirection.North);
    }
}