using System.Drawing;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Memento;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;
using BroadwayBB.Simulation.Memento;

namespace BroadwayBB.Common.Entities.Museum;

public class Museum
{
    public readonly MuseumConfiguration Config = new();
    private readonly TileManager _tileManager = new();
    private readonly AttendeeManager _attendeeManager = new();
    
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
        if (!Config.Get(ConfigType.ShouldMoveAttendees)) return;
        
        foreach (var attendee in Attendees)
        {
            var possibleDirections = _tileManager.GetAllowedRelativeTilePositions(
                attendee.Movement.GridPos
                );
            
            attendee.Movement.IsColliding = false;
            HandleAttendeeMovement(attendee, possibleDirections);
        }
        
        _attendeeManager.HandleCollision();
        _attendeeManager.HandleAttendeeQueue();
    }

    private void HandleAttendeeMovement(IAttendee attendee, List<MovementDirection> possibleDirections)
    {
        var movementResult = attendee.Movement.HandleMovement(possibleDirections);
        if (movementResult.HasEnteredNewGridTile) HandleTileCollision(attendee, movementResult);
    }

    private void HandleTileCollision(IAttendee attendee, MovementResult movementResult)
    {
        var tileCollisionResult = _tileManager.HandleCollision(movementResult.GridPos, Config.Get(ConfigType.ShouldHaveTileBehavior));
        if (!Config.Get(ConfigType.ShouldRenderAttendees)) tileCollisionResult.ShouldCreateArtist = false;
        if (!Config.Get(ConfigType.ShouldCollideWithPath)) tileCollisionResult.IsInPath = false;
        _attendeeManager.HandleTileCollisionResult(tileCollisionResult, attendee);
    }

    public void HandleMouseTileUpdate(Coords mouseGridPos)
    {
        if (!Config.Get(ConfigType.ShouldHaveTileBehavior)) return;

        var tileCollisionResult = _tileManager.HandleCollision(mouseGridPos, Config.Get(ConfigType.ShouldHaveTileBehavior));

        tileCollisionResult.ShouldRemoveArtist = false;
        // We can pass a new "non-existing" attendee here since removing artists is always disabled.
        _attendeeManager.HandleTileCollisionResult(
            tileCollisionResult, 
            new Artist(new Coords(0,0),0,0));
    }

    public int GetMaxAttendees() => _attendeeManager.AttendeeLimit;
    
    public List<DebugTile> GetDebugInfo()
    {
        var debugInfo = new List<DebugTile>();
        if (Config.Get(ConfigType.ShouldRenderQuadtree)) debugInfo.AddRange(_attendeeManager.GetColliderDebugInfo());
        if (Config.Get(ConfigType.ShouldRenderPath)) 
            debugInfo.AddRange(_tileManager.GetPathfinderDebugInfo(Config.Get(ConfigType.ShouldRenderVisited)));
        
        return debugInfo;
    } 
    
    public MuseumMemento CreateMemento()
    {
        var tiles = _tileManager.CreateMemento();
        var attendees = _attendeeManager.CreateMemento();
        return new MuseumMemento(tiles, attendees);
    }

    public void RewindMemento(MuseumMemento lastMemento)
    {
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

    public void SetData(List<ITile> tiles, List<IAttendee> artists)
    {
        _tileManager.Tiles = tiles;
        _attendeeManager.InitCollider(tiles.Max(tile => tile.Pos.Xi) + 1, tiles.Max(tile => tile.Pos.Yi) + 1);
        
        Config.AddObserver(_attendeeManager.AttendeeCollider);
        Config.AddObserver(_tileManager.TilePathfinder);
        
        SetAttendeeLimit();
        _attendeeManager.Attendees = artists;
    }

    public void GenerateTilePath(Coords leftClickPosition, Coords rightClickPosition)
    {
        _tileManager.GeneratePath(leftClickPosition, rightClickPosition);
    }
}