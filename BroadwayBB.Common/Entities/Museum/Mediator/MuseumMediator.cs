using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Museum.Mediator;

public class MuseumMediator : IMuseumMediator
{
    private readonly TileManager _tileManager;
    private readonly AttendeeManager _attendeeManager;
    private readonly MuseumConfiguration _museumConfiguration;

    public MuseumMediator(TileManager tileManager, AttendeeManager attendeeManager, MuseumConfiguration museumConfiguration)
    {
        _tileManager = tileManager;
        _attendeeManager = attendeeManager;
        _museumConfiguration = museumConfiguration;
        
        _tileManager.SetMuseumMediator(this);
        _attendeeManager.SetMuseumMediator(this);
    }

    public void Notify(NotificationBase notification)
    {
        switch (notification)
        {
            case AttendeeMovementNotification movementNotification:
                ProcessMovementLogic(movementNotification);
                break;
            case MovementFinishedNotification movementFinishedNotification:
                ProcessMovementFinishedLogic(movementFinishedNotification);
                break;
            case TileCollisionNotification tileCollisionNotification:
                ProcessTileCollisionLogic(tileCollisionNotification);
                break;
        }
    }

    private void ProcessMovementLogic(AttendeeMovementNotification notification)
    {
        var movementResult = notification.Attendee.Movement.HandleMovement(notification.PossibleDirections);
        if (movementResult.HasEnteredNewGridTile);
        var tileCollisionResult = _tileManager.HandleCollision(movementResult.GridPos, _museumConfiguration.Get(ConfigType.ShouldHaveTileBehavior));
        if (!_museumConfiguration.Get(ConfigType.ShouldRenderAttendees)) tileCollisionResult.ShouldCreateArtist = false;
        if (!_museumConfiguration.Get(ConfigType.ShouldCollideWithPath)) tileCollisionResult.IsInPath = false;
            
        _attendeeManager.HandleTileCollisionResult(tileCollisionResult, notification.Attendee);
    }

    private void ProcessMovementFinishedLogic(MovementFinishedNotification notification)
    {
        _attendeeManager.HandleCollision();
        _attendeeManager.HandleAttendeeQueue();
    }

    private void ProcessTileCollisionLogic(TileCollisionNotification notification)
    {
        notification.TileCollisionResult.ShouldRemoveArtist = false;
        // We can pass a new "non-existing" attendee here since removing artists is always disabled.
        _attendeeManager.HandleTileCollisionResult(
            notification.TileCollisionResult, 
            new Artist(new Coords(0,0),0,0));
    }
}