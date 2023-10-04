using System.Drawing;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Museum;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Simulation;

public class MuseumSimulation : IMuseumSimulation
{
    private readonly List<ISimulationObserver> _observers = new();
    private Timer _simulationTickTimer, _simulationBackgroundRefreshTimer, _simulationDebugInfoTimer, _mementoCreationTimer;
    private readonly int _simulationIntervalMilliseconds = 150;
    private int _currentTick;
    private readonly int _timeSkipTickAmount = 10;
    private readonly int _backgroundUpdateIntervalMilliseconds = 400;
    private readonly int _debugInfoUpdateIntervalMilliseconds = 500;
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
        _simulationDebugInfoTimer = new Timer(NotifyDebugInfoUpdate, null, 0, _debugInfoUpdateIntervalMilliseconds);
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

    public void ToggleConfigValue(ConfigType type) => _museum.Config.Toggle(type);
    
    public void HandlePointerClick(bool isLeftMouse, MouseGridPosition mouseGridPosition)
    {
        
    }

    public void UpdateTile(MouseGridPosition mouseGridPosition) => _museum.HandleMouseTileUpdate(mouseGridPosition.PosX, mouseGridPosition.PosY);

    public List<ITile> GetMuseumTiles() => _museum.Tiles;

    public List<DebugTile> GetDebugInfo() => _museum.GetDebugInfo();
    
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

    private void NotifyDebugInfoUpdate(object? state) => _observers.ForEach(observer => observer.UpdateDebugInfo());
    
    private void NotifyStopped() => _observers.ForEach(observer => observer.StopSimulation());

    private void NotifyOpenShortcutMenu() => _observers.ForEach(observer => observer.OpenShortcutMenu());
}