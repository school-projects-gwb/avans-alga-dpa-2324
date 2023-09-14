using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Common.Entities.Extensions;

public static class MovementExtensions
{
    public static int GetRoundedGridPosX(this IMovement movement)
    {
        return (int)Math.Floor(movement.GridPosX);
    }

    public static int GetRoundedGridPosY(this IMovement movement)
    {
        return (int)Math.Floor(movement.GridPosY);
    }
}