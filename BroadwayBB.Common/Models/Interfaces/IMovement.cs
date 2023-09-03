namespace BroadwayBB.Common.Models.Interfaces;

public interface IMovement
{
    public double GridPosX { get; }
    public double GridPosY { get; }
    public double SpeedVertical { get; }
    public double SpeedHorizontal { get; }
    public MovementDirection MovementDirection { get; }
    
    public void Move(List<MovementDirection> possibleDirections);
}