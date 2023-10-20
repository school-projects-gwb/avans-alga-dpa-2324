using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Museum.Mediator;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;
using BroadwayBB.Simulation.Memento;

namespace BroadwayBB.Common.Entities.Museum;

public class Museum
{
    public readonly MuseumConfiguration Config = new();
    private readonly MuseumMediator _museumMediator;

    public Museum() => _museumMediator = new MuseumMediator(Config);
    
    public List<ITile> Tiles
    {
        get => _museumMediator.TileManager.Tiles;
        set
        {
            _museumMediator.TileManager.Tiles = value;
            SetAttendeeLimit();
        }
    }

    public List<IAttendee> Attendees
    {
        get => _museumMediator.AttendeeManager.Attendees;
        set => _museumMediator.AttendeeManager.Attendees = value;
    }

    private void SetAttendeeLimit()
    {
        double limitRelativeToTileModifier = 0.075;
        int roundedLimit = (int)Math.Round(_museumMediator.TileManager.Tiles.Count() * limitRelativeToTileModifier);
        _museumMediator.AttendeeManager.SetAttendeeLimit(roundedLimit);
    }

    public void MoveAttendees()
    {
        if (Config.Get(ConfigType.ShouldMoveAttendees)) _museumMediator.TileManager.HandleMovement(Attendees);
    }

    public void HandleMouseTileUpdate(Coords mouseGridPos)
    {
        if (Config.Get(ConfigType.ShouldHaveTileBehavior))
            _museumMediator.TileManager.CheckCollision(mouseGridPos, Config.Get(ConfigType.ShouldHaveTileBehavior));
    }

    public int GetMaxAttendees() => _museumMediator.AttendeeManager.AttendeeLimit;
    
    public List<DebugTile> GetDebugInfo()
    {
        var debugInfo = new List<DebugTile>();
        if (Config.Get(ConfigType.ShouldRenderQuadtree)) debugInfo.AddRange(_museumMediator.AttendeeManager.GetColliderDebugInfo());
        if (Config.Get(ConfigType.ShouldRenderPath)) 
            debugInfo.AddRange(_museumMediator.TileManager.GetPathfinderDebugInfo(Config.Get(ConfigType.ShouldRenderVisited)));
        
        return debugInfo;
    } 
    
    public MuseumMemento CreateMemento()
    {
        var tiles = _museumMediator.TileManager.GetTileClones();
        var attendees = _museumMediator.AttendeeManager.GetAttendeeClones();
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
        _museumMediator.TileManager.Tiles = tiles;
        _museumMediator.AttendeeManager.InitCollider(tiles.Max(tile => tile.Pos.Xi) + 1, tiles.Max(tile => tile.Pos.Yi) + 1);
        
        Config.AddObserver(_museumMediator.AttendeeManager.AttendeeCollider);
        Config.AddObserver(_museumMediator.TileManager.TilePathfinder);
        
        SetAttendeeLimit();
        _museumMediator.AttendeeManager.Attendees = artists;
    }

    public void GenerateTilePath(Coords leftClickPosition, Coords rightClickPosition)
    {
        _museumMediator.TileManager.GeneratePath(leftClickPosition, rightClickPosition);
    }
}