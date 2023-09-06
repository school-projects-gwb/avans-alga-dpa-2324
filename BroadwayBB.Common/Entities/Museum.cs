using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities;

public class Museum
{
    public MuseumConfiguration MuseumConfiguration = new();
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
        if (!MuseumConfiguration.ShouldMoveAttendees) return;
        
        foreach (var attendee in Attendees)
        {
            var possibleDirections = _tileManager.GetRelativeTilePositions(
                attendee.Movement.GetRoundedGridPosX(), 
                attendee.Movement.GetRoundedGridPosY()
                );
            var movementResult = attendee.Movement.HandleMovement(possibleDirections);
            if (!movementResult.HasEnteredNewGridTile) continue;
            
            var tileCollisionResult = _tileManager.HandleCollision(movementResult.GridPosX, movementResult.GridPosY);
            if (!MuseumConfiguration.ShouldRenderAttendees) tileCollisionResult.ShouldCreateArtist = false;
            _attendeeManager.HandleTileCollisionResult(tileCollisionResult, attendee);
        }

        _attendeeManager.HandleAttendeeQueue();
    }
}