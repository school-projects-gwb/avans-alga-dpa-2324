using System.Drawing;
using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Memento;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Quadtree;

namespace BroadwayBB.Common.Entities;

public class Museum
{
    public readonly MuseumConfiguration MuseumConfiguration = new();
    private readonly TileManager _tileManager = new();
    private readonly AttendeeManager _attendeeManager = new();
    private readonly MementoCaretaker _mementoCaretaker = new();
    private Quadtree<IAttendee> _attendeeQuadtree = new(0, new Rectangle(0, 0, 53, 53));
    
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

    public List<Rectangle> GetDebugInfo()
    {
        var debugInfo = new List<Rectangle>();
        if (MuseumConfiguration.ShouldRenderQuadtree) debugInfo = _attendeeQuadtree.GetNodeCoordinates();
        
        return debugInfo;
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
        
        _attendeeQuadtree.Clear();
        
        foreach (var attendee in Attendees)
        {
            var possibleDirections = _tileManager.GetAllowedRelativeTilePositions(
                attendee.Movement.GetRoundedGridPosX(), 
                attendee.Movement.GetRoundedGridPosY()
                );
            
            HandleAttendeeMovement(attendee, possibleDirections);
            
            _attendeeQuadtree.Insert(
                attendee, 
                attendee.Movement.GetRoundedGridPosX(), 
                attendee.Movement.GetRoundedGridPosY()
                );
        }
        
        _attendeeManager.HandleAttendeeQueue();
    }

    private void HandleAttendeeMovement(IAttendee attendee, List<MovementDirection> possibleDirections)
    {
        var movementResult = attendee.Movement.HandleMovement(possibleDirections);
        _attendeeManager.HandleCollision(attendee.Movement.GridPosX, attendee.Movement.GridPosY);
        if (movementResult.HasEnteredNewGridTile) HandleTileCollision(attendee, movementResult);
    }

    private void HandleTileCollision(IAttendee attendee, MovementResult movementResult)
    {
        var tileCollisionResult = _tileManager.HandleCollision(movementResult.GridPosX, movementResult.GridPosY);
        if (!MuseumConfiguration.ShouldRenderAttendees) tileCollisionResult.ShouldCreateArtist = false;
        _attendeeManager.HandleTileCollisionResult(tileCollisionResult, attendee);
    }

    public void HandleMouseTileUpdate(int mouseGridPosX, int mouseGridPosY)
    {
        var tileCollisionResult = _tileManager.HandleCollision(mouseGridPosX, mouseGridPosY);
        tileCollisionResult.ShouldRemoveArtist = false;
        // We can pass a new "non-existing" attendee here since removing artists is always disabled.
        _attendeeManager.HandleTileCollisionResult(
            tileCollisionResult, 
            new Artist(0,0,0,0));
    }

    public int GetMaxAttendees() => _attendeeManager.AttendeeLimit;

    public void CreateMemento()
    {
        var tiles = _tileManager.CreateMemento();
        var attendees = _attendeeManager.CreateMemento();
        _mementoCaretaker.AddMemento(new MuseumMemento(tiles, attendees));
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