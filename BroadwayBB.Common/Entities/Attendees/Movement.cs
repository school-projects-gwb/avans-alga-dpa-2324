using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Attendees;

public class Movement : IMovement
{
    public bool IsColliding { get; set; }
    public Coords GridPos { get; private set; }

    private double SpeedVertical { get; }
    private double SpeedHorizontal { get; }
    private MovementDirection MovementDirection { get; set; }
    
    private readonly double _decimalSpeedMultiplier = 0.1, _minimumSpeedMultiplierValue = 1;

    public Movement(Coords coords, double speedVertical, double speedHorizontal, MovementDirection movementDirection)
    {
        GridPos = coords;
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
        int previousPosX = this.GridPos.Xi, previousPosY = this.GridPos.Yi;
        Coords verticalMovement = new Coords(0, SpeedVertical), horizontalMovement = new Coords(SpeedHorizontal, 0);

             if (MovementDirection == MovementDirection.North) GridPos -= verticalMovement;
        else if (MovementDirection == MovementDirection.South) GridPos += verticalMovement;
        else if (MovementDirection == MovementDirection.West ) GridPos -= horizontalMovement;
        else if (MovementDirection == MovementDirection.East ) GridPos += horizontalMovement;

        return new MovementResult
        {
            HasEnteredNewGridTile = previousPosX != this.GridPos.Xi || previousPosY != this.GridPos.Yi,
            GridPos = this.GridPos
        };
    }

    private double SpeedToDecimal(double target) => target >= _minimumSpeedMultiplierValue ? target *_decimalSpeedMultiplier : target;

    public IMovement DeepCopy() => new Movement(GridPos, SpeedVertical, SpeedHorizontal, MovementDirection);
}