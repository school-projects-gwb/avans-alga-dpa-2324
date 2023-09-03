using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models;

public class Movement : IMovement
{
    public int GridPosX { get; private set; }
    public int GridPosY { get; private set; }
    public int PosXOnTile { get; private set; }
    public int PosYOnTile { get; private set; }
    public int SpeedVertical { get; }
    public int SpeedHorizontal { get; }
    public MovementDirection MovementDirection { get; private set; }

    private readonly int _minPosOnTile = 0, _maxPosOnTile = 5;

    public Movement(int posX, int posY, int speedVertical, int speedHorizontal, MovementDirection movementDirection)
    {
        GridPosX = posX;
        GridPosY = posY;
        SpeedVertical = speedVertical;
        SpeedHorizontal = speedHorizontal;
        PosXOnTile = PosYOnTile = 0;
        MovementDirection = movementDirection;
    }
    
    public void Move(List<MovementDirection> possibleDirections)
    {
        if (possibleDirections.Count == 0) return;
        
        UpdateMovementDirection(possibleDirections);
        UpdatePositionOnTile();
        UpdatePositionOnGrid();
    }
    
    private void UpdateMovementDirection(List<MovementDirection> possibleDirections)
    {
        if (possibleDirections.Contains(MovementDirection)) return;
        
        int randomPositionIndex = new Random().Next(possibleDirections.Count);
        MovementDirection = possibleDirections[randomPositionIndex];
    }
    
    private void UpdatePositionOnTile()
    {
        if (MovementDirection == MovementDirection.North) PosYOnTile -= SpeedVertical;
        if (MovementDirection == MovementDirection.South) PosYOnTile += SpeedVertical;
        if (MovementDirection == MovementDirection.West) PosXOnTile -= SpeedHorizontal;
        if (MovementDirection == MovementDirection.East) PosXOnTile += SpeedHorizontal;
    }

    private void UpdatePositionOnGrid()
    {
        if (PosXOnTile < _minPosOnTile)
        {
            GridPosX--;
            PosXOnTile = _maxPosOnTile;
        }
        else if (PosXOnTile > _maxPosOnTile)
        {
            GridPosX++;
            PosXOnTile = _minPosOnTile;
        }

        if (PosYOnTile < _minPosOnTile)
        {
            GridPosY--;
            PosYOnTile = _maxPosOnTile;
        }
        else if (PosYOnTile > _maxPosOnTile)
        {
            GridPosY++;
            PosYOnTile = _minPosOnTile;
        }
    }
}