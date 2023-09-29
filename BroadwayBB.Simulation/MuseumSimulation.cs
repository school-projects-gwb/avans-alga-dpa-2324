using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation;

public class MuseumSimulation : IMuseumSimulation
{
    private readonly List<ISimulationObserver> _observers = new();
    private Timer _simulationTickTimer;
    private Timer _simulationBackgroundRefreshTimer;
    private Timer _mementoCreationTimer;
    private readonly int _simulationIntervalMilliseconds = 150;
    private int _currentTick = 0;
    private readonly int _timeSkipTickAmount = 10;
    private readonly int _backgroundUpdateIntervalMilliseconds = 400;
    private readonly int _mementoCreationIntervalMilliseconds = 500;
    private readonly Museum _museum;

    public MuseumSimulation(Museum museum)
    {
        _museum = museum;
        InitializeSimulationTimers();
    }
    
    private void InitializeSimulationTimers()
    {
        _simulationTickTimer = new Timer(Simulate, null, 0, _simulationIntervalMilliseconds);
        _simulationBackgroundRefreshTimer = new Timer(NotifyBackgroundUpdate, null, 0, _backgroundUpdateIntervalMilliseconds);
        _mementoCreationTimer = new Timer(CreateMemento, null, 0, _mementoCreationIntervalMilliseconds);
    }

    private void CreateMemento(object? state)
    {
        _currentTick++;
        if (_currentTick != _timeSkipTickAmount) return;

        lock (_museum) _museum.CreateMemento();
        
        _currentTick = 0;
    }
    
    private void Simulate(object? state)
    {
        lock (_museum) _museum.MoveAttendees();
        NotifyAttendeeUpdated();
    }
    
    public void ToggleAttendeeMovement()
    {
        _museum.MuseumConfiguration.ShouldMoveAttendees = !_museum.MuseumConfiguration.ShouldMoveAttendees;
    }

    public void ToggleAttendeeRendering()
    {
        _museum.MuseumConfiguration.ShouldRenderAttendees = !_museum.MuseumConfiguration.ShouldRenderAttendees;
    }

    public void UpdateTile(MouseGridPosition mouseGridPosition)
    {
        _museum.HandleMouseTileUpdate(mouseGridPosition.PosX, mouseGridPosition.PosY);
    }

    public List<ITile> GetMuseumTiles() => _museum.Tiles;
    
    public List<IAttendee> GetMuseumAttendees() => _museum.Attendees;

    public int GetMaxMuseumAttendees() => _museum.GetMaxAttendees();

    public void OpenFileMenu() => NotifyStopped();

    public void OpenShortcutMenu() => NotifyOpenShortcutMenu();

    public void FastForward()
    {
        for (int i = 0; i < _timeSkipTickAmount; i++) 
            lock (_museum) _museum.MoveAttendees();
    }

    public void Rewind()
    {
        lock (_museum) _museum.RewindMemento();
    }
    
    public void Subscribe(ISimulationObserver observer) => _observers.Add(observer);

    private void NotifyAttendeeUpdated() => _observers.ForEach(observer => observer.UpdateSimulation());
    
    private void NotifyBackgroundUpdate(object? state) => _observers.ForEach(observer => observer.UpdateBackground());

    private void NotifyStopped() => _observers.ForEach(observer => observer.StopSimulation());

    private void NotifyOpenShortcutMenu() => _observers.ForEach(observer => observer.OpenShortcutMenu());
}