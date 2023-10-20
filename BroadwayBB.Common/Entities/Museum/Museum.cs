using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Museum.Mediator;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;
using BroadwayBB.Simulation.Memento;

namespace BroadwayBB.Common.Entities.Museum;

public class Museum
{
    public readonly MuseumConfiguration Config = new();
    private readonly TileManager _tileManager = new();
    private readonly AttendeeManager _attendeeManager = new();
    private readonly MuseumMediator _museumMediator;

    public Museum() => _museumMediator = new MuseumMediator(_tileManager, _attendeeManager, Config);
    
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
        if (Config.Get(ConfigType.ShouldMoveAttendees)) _tileManager.HandleMovement(Attendees);
    }

    public void HandleMouseTileUpdate(Coords mouseGridPos)
    {
        if (Config.Get(ConfigType.ShouldHaveTileBehavior))
            _tileManager.CheckCollision(mouseGridPos, Config.Get(ConfigType.ShouldHaveTileBehavior));
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
        var tiles = _tileManager.GetTileClones();
        var attendees = _attendeeManager.GetAttendeeClones();
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