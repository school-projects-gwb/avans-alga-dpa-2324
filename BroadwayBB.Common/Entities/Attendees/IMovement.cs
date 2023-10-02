using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Attendees;

public interface IMovement
{
    public bool IsColliding { get; set; }
    public double GridPosX { get; }
    
    public double GridPosY { get; }
    
    public MovementResult HandleMovement(List<MovementDirection> possibleDirections);

    public IMovement DeepCopy();
}