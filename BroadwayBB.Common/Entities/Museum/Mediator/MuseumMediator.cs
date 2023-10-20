using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Museum.Mediator;

public class MuseumMediator : IMuseumMediator
{
    private readonly MuseumConfiguration _museumConfiguration;

    public TileManager TileManager { get; private set; }
    public AttendeeManager AttendeeManager { get; private set; }
    
    public MuseumMediator(MuseumConfiguration museumConfiguration)
    {
        TileManager = new(this);
        AttendeeManager = new(this);
        _museumConfiguration = museumConfiguration;
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
        var tileCollisionResult = TileManager.HandleCollision(movementResult.GridPos, _museumConfiguration.Get(ConfigType.ShouldHaveTileBehavior));
        if (!_museumConfiguration.Get(ConfigType.ShouldRenderAttendees)) tileCollisionResult.ShouldCreateArtist = false;
        if (!_museumConfiguration.Get(ConfigType.ShouldCollideWithPath)) tileCollisionResult.IsInPath = false;
            
        AttendeeManager.HandleTileCollisionResult(tileCollisionResult, notification.Attendee);
    }

    private void ProcessMovementFinishedLogic(MovementFinishedNotification notification)
    {
        AttendeeManager.HandleCollision();
        AttendeeManager.HandleAttendeeQueue();
    }

    private void ProcessTileCollisionLogic(TileCollisionNotification notification)
    {
        notification.TileCollisionResult.ShouldRemoveArtist = false;
        // We can pass a new "non-existing" attendee here since removing artists is always disabled.
        AttendeeManager.HandleTileCollisionResult(
            notification.TileCollisionResult, 
            new Artist(new Coords(0,0),0,0));
    }
}