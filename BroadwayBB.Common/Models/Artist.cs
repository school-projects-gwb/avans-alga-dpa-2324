using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models;

public class Artist : IAttendee
{
    public int PosX { get; }
    public int PosY { get; }
    public int SpeedVertical { get; }
    public int SpeedHorizontal { get; }

    public Artist(int posX, int posY, int speedVertical, int speedHorizontal)
    {
        PosX = posX;
        PosY = posY;
        SpeedVertical = speedVertical;
        SpeedHorizontal = speedHorizontal;
    }
}