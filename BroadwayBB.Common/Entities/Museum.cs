using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Memento;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities;

public class Museum
{
    public readonly MuseumConfiguration MuseumConfiguration = new();
    private readonly TileManager _tileManager = new();
    private readonly AttendeeManager _attendeeManager = new();
    private readonly MementoCaretaker _mementoCaretaker = new();
    
    public List<ITile> Tiles
    {
        get => _tileManager.Tiles;
        set
        {
            _tileManager.Tiles = value;
            SetAttendeeLimit();
        }
    }

    public List<IAttendee> Attendees
    {
        get => _attendeeManager.Attendees;
        set => _attendeeManager.Attendees = value;
    }

    private void SetAttendeeLimit()
    {
        double limitRelativeToTileModifier = 0.075;
        int roundedLimit = (int)Math.Round(_tileManager.Tiles.Count() * limitRelativeToTileModifier);
        _attendeeManager.SetAttendeeLimit(roundedLimit);
    }

    public void MoveAttendees()
    {
        if (!MuseumConfiguration.ShouldMoveAttendees) return;
        
        foreach (var attendee in Attendees)
        {
            var possibleDirections = _tileManager.GetAllowedRelativeTilePositions(
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

    public void HandleMouseTileUpdate(int mouseGridPosX, int mouseGridPosY)
    {
        var tileCollisionResult = _tileManager.HandleCollision(mouseGridPosX, mouseGridPosY);
        tileCollisionResult.ShouldRemoveArtist = false;
        // We can pass a new "non-existing" attendee here since removing artists is always disabled.
        // This is not the cleanest way but works well enough in this one single specific situation.
        _attendeeManager.HandleTileCollisionResult(
            tileCollisionResult, 
            new Artist(0,0,0,0));
    }

    public int GetMaxAttendees() => _attendeeManager.AttendeeLimit;

    public void CreateMemento()
    {
        lock (this) {
            var tiles = _tileManager.CreateMemento();
            var attendees = _attendeeManager.CreateMemento();
            _mementoCaretaker.AddMemento(new MuseumMemento(tiles, attendees));
        }
    }

    public void RewindMemento()
    {
        var lastMemento = _mementoCaretaker.GetMemento();
        if (lastMemento == null) return;

        lock (Tiles)
        {
            Tiles.Clear();
            Tiles = lastMemento.Tiles;
        }

        lock (Attendees)
        {
            Attendees.Clear();
            Attendees = lastMemento.Attendees;
        }
    }
}