using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models;

public class Movement : IMovement
{
    public double GridPosX { get; private set; }
    public double GridPosY { get; private set; }
    public double SpeedVertical { get; }
    public double SpeedHorizontal { get; }
    public MovementDirection MovementDirection { get; private set; }
    
    private readonly double _decimalSpeedMultiplier = 0.1, _minimumSpeedMultiplierValue = 1;

    public Movement(double posX, double posY, double speedVertical, double speedHorizontal, MovementDirection movementDirection)
    {
        GridPosX = posX;
        GridPosY = posY;
        SpeedVertical = SpeedToDecimal(speedVertical);
        SpeedHorizontal = SpeedToDecimal(speedHorizontal);
        MovementDirection = movementDirection;
    }

    public void Move(List<MovementDirection> possibleDirections)
    {
        if (possibleDirections.Count == 0) return;
        
        UpdateMovementDirection(possibleDirections);
        UpdatePositionOnGrid();
    }

    private void UpdateMovementDirection(List<MovementDirection> possibleDirections)
    {
        if (possibleDirections.Contains(MovementDirection)) return;

        int randomPositionIndex = new Random().Next(possibleDirections.Count);
        MovementDirection = possibleDirections[randomPositionIndex];
    }

    private void UpdatePositionOnGrid()
    {
        double movementVertical = SpeedVertical;
        double movementHorizontal = SpeedHorizontal;

        if (MovementDirection == MovementDirection.North) GridPosY -= movementVertical;
        if (MovementDirection == MovementDirection.South) GridPosY += movementVertical;
        if (MovementDirection == MovementDirection.West) GridPosX -= movementHorizontal;
        if (MovementDirection == MovementDirection.East) GridPosX += movementHorizontal;
    }

    private double SpeedToDecimal(double target) => target >= _minimumSpeedMultiplierValue ? target *_decimalSpeedMultiplier : target;
}