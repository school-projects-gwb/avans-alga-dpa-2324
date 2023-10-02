using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Interfaces;

public interface IMovement
{
    public double GridPosX { get; }
    
    public double GridPosY { get; }
    
    public MovementResult HandleMovement(List<MovementDirection> possibleDirections);

    public IMovement DeepCopy();
}