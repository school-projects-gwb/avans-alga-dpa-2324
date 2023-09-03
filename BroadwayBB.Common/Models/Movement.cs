using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models;

public class Movement : IMovement
{
    public int GridPosX { get; }
    public int GridPosY { get; }
    public int PosXOnGrid { get; }
    public int PosYOnGrid { get; }
    public int SpeedVertical { get; }
    public int SpeedHorizontal { get; }

    public Movement(int posX, int posY, int speedVertical, int speedHorizontal)
    {
        GridPosX = posX;
        GridPosY = posY;
        SpeedVertical = speedVertical;
        SpeedHorizontal = speedHorizontal;
        PosXOnGrid = PosYOnGrid = 0;
    }
}