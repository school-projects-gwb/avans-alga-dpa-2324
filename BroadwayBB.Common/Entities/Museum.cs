using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities;

public class Museum
{
    private readonly TileManager _tileManager = new();
    private readonly AttendeeManager _attendeeManager = new();
    
    public List<ITile> Tiles { 
        get => _tileManager.Tiles;
        set => _tileManager.Tiles = value;
    }

    public List<IAttendee> Attendees
    {
        get => _attendeeManager.Attendees;
        set => _attendeeManager.Attendees = value;
    }

    public void MoveAttendees()
    {
        foreach (var attendee in Attendees)
        {
            int currentPosX = (int) Math.Floor(attendee.Movement.GridPosX);
            int currentPosY = (int) Math.Floor(attendee.Movement.GridPosY);
            var possibleDirections = _tileManager.GetRelativeTilePositions(currentPosX, currentPosY);
            var movementResult = attendee.Movement.HandleMovement(possibleDirections);
            if (movementResult.HasEnteredNewGridTile) HandleAttendeeMovementCollision(movementResult);
        }
    }
    
    private void HandleAttendeeMovementCollision(MovementResult movementResult)
    {
        var tileCollisionResult = _tileManager.HandleCollision(movementResult.GridPosX, movementResult.GridPosY);
    }
}