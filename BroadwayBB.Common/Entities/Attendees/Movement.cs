using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Attendees;

public class Movement : IMovement
{
    public double GridPosX { get; private set; }
    public double GridPosY { get; private set; }
    private double SpeedVertical { get; }
    private double SpeedHorizontal { get; }
    private MovementDirection MovementDirection { get; set; }
    
    private readonly double _decimalSpeedMultiplier = 0.1, _minimumSpeedMultiplierValue = 1;

    public Movement(double posX, double posY, double speedVertical, double speedHorizontal, MovementDirection movementDirection)
    {
        GridPosX = posX;
        GridPosY = posY;
        SpeedVertical = SpeedToDecimal(speedVertical);
        SpeedHorizontal = SpeedToDecimal(speedHorizontal);
        MovementDirection = movementDirection;
    }

    public MovementResult HandleMovement(List<MovementDirection> possibleDirections)
    {
        if (possibleDirections.Count == 0) return new MovementResult();
        
        UpdateMovementDirection(possibleDirections);
        return UpdatePositionOnGrid();
    }

    private void UpdateMovementDirection(List<MovementDirection> possibleDirections)
    {
        var allowedDirections = possibleDirections.Where(HasSpeedForDirection).ToList();
        if (allowedDirections.Contains(MovementDirection) || possibleDirections.Count == 0 || allowedDirections.Count == 0) return;

        int randomPositionIndex = new Random().Next(
            allowedDirections.Count);
        MovementDirection = allowedDirections[randomPositionIndex];
    }

    private bool HasSpeedForDirection(MovementDirection direction)
    {
        if (direction == MovementDirection.North || direction == MovementDirection.South)
            return SpeedVertical > 0.0;
        
        return SpeedHorizontal > 0.0;
    }

    private MovementResult UpdatePositionOnGrid()
    {
        int previousPosX = this.GetRoundedGridPosX(), previousPosY = this.GetRoundedGridPosY(); 
        double movementVertical = SpeedVertical, movementHorizontal = SpeedHorizontal;

        if (MovementDirection == MovementDirection.North) GridPosY -= movementVertical;
        if (MovementDirection == MovementDirection.South) GridPosY += movementVertical;
        if (MovementDirection == MovementDirection.West) GridPosX -= movementHorizontal;
        if (MovementDirection == MovementDirection.East) GridPosX += movementHorizontal;

        return new MovementResult
        {
            HasEnteredNewGridTile = previousPosX != this.GetRoundedGridPosX() || previousPosY != this.GetRoundedGridPosY(),
            GridPosX = this.GetRoundedGridPosX(),
            GridPosY = this.GetRoundedGridPosY()
        };
    }

    private double SpeedToDecimal(double target) => target >= _minimumSpeedMultiplierValue ? target *_decimalSpeedMultiplier : target;

    public IMovement DeepCopy() => new Movement(GridPosX, GridPosY, SpeedVertical, SpeedHorizontal, MovementDirection);
}