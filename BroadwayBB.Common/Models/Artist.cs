using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models;

public class Artist : IAttendee
{
    public IMovement Movement { get; }
    public Artist(int posX, int posY, int speedVertical, int speedHorizontal)
    {
        Movement = new Movement(posX, posY, speedVertical, speedHorizontal);
    }

    
}