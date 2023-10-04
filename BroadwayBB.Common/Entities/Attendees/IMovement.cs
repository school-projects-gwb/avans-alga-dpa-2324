using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Attendees;

public interface IMovement
{
    public bool IsColliding { get; set; }
    public Coords GridPos { get; }
    
    public MovementResult HandleMovement(List<MovementDirection> possibleDirections);

    public IMovement DeepCopy();
}