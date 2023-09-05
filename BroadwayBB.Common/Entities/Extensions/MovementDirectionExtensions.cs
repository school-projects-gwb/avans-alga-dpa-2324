namespace BroadwayBB.Common.Entities.Extensions;

public static class MovementDirectionExtensions
{
    private static readonly Random Random = new();

    public static MovementDirection GetRandomDirection()
    {
        Array values = Enum.GetValues(typeof(MovementDirection));
        return (MovementDirection)(values.GetValue(Random.Next(values.Length)) ?? MovementDirection.North);
    }
}