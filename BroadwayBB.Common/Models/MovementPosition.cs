namespace BroadwayBB.Common.Models;

public class MovementPosition
{
    public int GridPosX { get; }
    public int GridPosY { get; }
    public int PosXOnGrid { get; }
    public int PosYOnGrid { get; }
    public MovementDirection MovementDirection { get; }

    public MovementPosition(int gridPosX, int gridPosY, int posXOnGrid, int posYOnGrid)
    {
        GridPosX = gridPosX;
        GridPosY = gridPosY;
        PosXOnGrid = posXOnGrid;
        PosYOnGrid = posYOnGrid;
        MovementDirection = MovementDirection.North;
    }
}