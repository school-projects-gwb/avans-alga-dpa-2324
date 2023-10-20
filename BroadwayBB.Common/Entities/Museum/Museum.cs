using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Museum.Mediator;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;
using BroadwayBB.Simulation.Memento;

namespace BroadwayBB.Common.Entities.Museum;

public class Museum
{
    public readonly MuseumConfiguration Config = new();
    private readonly MuseumMediator _mediator;

    public Museum() => _mediator = new MuseumMediator(Config);
    
    public List<ITile> Tiles
    {
        get => _mediator.TileManager.Tiles;
        set
        {
            _mediator.TileManager.Tiles = value;
            SetAttendeeLimit();
        }
    }

    public List<IAttendee> Attendees
    {
        get => _mediator.AttendeeManager.Attendees;
        set => _mediator.AttendeeManager.Attendees = value;
    }

    private void SetAttendeeLimit()
    {
        double limitRelativeToTileModifier = 0.075;
        int roundedLimit = (int)Math.Round(_mediator.TileManager.Tiles.Count() * limitRelativeToTileModifier);
        _mediator.AttendeeManager.SetAttendeeLimit(roundedLimit);
    }

    public void MoveAttendees()
    {
        if (Config.Get(ConfigType.ShouldMoveAttendees)) _mediator.TileManager.HandleMovement(Attendees);
    }

    public void HandleMouseTileUpdate(Coords mouseGridPos)
    {
        if (Config.Get(ConfigType.ShouldHaveTileBehavior))
            _mediator.TileManager.CheckCollision(mouseGridPos, Config.Get(ConfigType.ShouldHaveTileBehavior));
    }

    public int GetMaxAttendees() => _mediator.AttendeeManager.AttendeeLimit;
    
    public List<DebugTile> GetDebugInfo()
    {
        var debugInfo = new List<DebugTile>();
        if (Config.Get(ConfigType.ShouldRenderQuadtree)) debugInfo.AddRange(_mediator.AttendeeManager.GetColliderDebugInfo());
        if (Config.Get(ConfigType.ShouldRenderPath)) 
            debugInfo.AddRange(_mediator.TileManager.GetPathfinderDebugInfo(Config.Get(ConfigType.ShouldRenderVisited)));
        
        return debugInfo;
    } 
    
    public MuseumMemento CreateMemento()
    {
        var tiles = _mediator.TileManager.GetTileClones();
        var attendees = _mediator.AttendeeManager.GetAttendeeClones();
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
        _mediator.TileManager.Tiles = tiles;
        _mediator.AttendeeManager.InitCollider(tiles.Max(tile => tile.Pos.Xi) + 1, tiles.Max(tile => tile.Pos.Yi) + 1);
        
        Config.AddObserver(_mediator.AttendeeManager.AttendeeCollider);
        Config.AddObserver(_mediator.TileManager.TilePathfinder);
        
        SetAttendeeLimit();
        _mediator.AttendeeManager.Attendees = artists;
    }

    public void GenerateTilePath(Coords leftClickPosition, Coords rightClickPosition)
    {
        _mediator.TileManager.GeneratePath(leftClickPosition, rightClickPosition);
    }
}