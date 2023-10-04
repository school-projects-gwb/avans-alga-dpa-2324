using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Attendees;

public class MovementResult
{
    public bool HasEnteredNewGridTile = false;
    public Coords GridPos = new Coords(0, 0);
}