using System.Drawing;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Memento;
using BroadwayBB.Common.Entities.Museum;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Simulation;

public class MuseumSimulationFacade : IMuseumSimulationFacade
{
    private readonly List<ISimulationObserver> _observers = new();
    private Timer? _simulationTickTimer, _simulationBackgroundRefreshTimer, _simulationDebugInfoTimer, _mementoCreationTimer;
    private readonly int _simulationIntervalMilliseconds = 150;
    private int _currentTick;
    private readonly int _timeSkipTickAmount = 10;
    private readonly int _backgroundUpdateIntervalMilliseconds = 500;
    private readonly int _debugInfoUpdateIntervalMilliseconds = 500;
    private readonly int _mementoCreationIntervalMilliseconds = 500;
    private readonly Museum _museum;
    private readonly PointerRegistration _pointerRegistration = new();
    private readonly MementoCaretaker _mementoCaretaker;

    public MuseumSimulationFacade(Museum museum)
    {
        _museum = museum;
        _mementoCaretaker = new MementoCaretaker(museum);
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

        CreateMemento();
        
        _currentTick = 0;
    }

    public void CreateMemento()
    {
        lock (_museum) _mementoCaretaker.AddMemento();
    }
    
    public void RewindMemento()
    {
        lock (_museum) _mementoCaretaker.RestoreMemento();
    }
    
    private void Simulate(object? state)
    {
        lock (_museum) _museum.MoveAttendees();
        
        NotifyAttendeeUpdated();
    }

    public void ToggleConfigValue(ConfigType type) => _museum.Config.Toggle(type);
    
    public void HandlePointerClick(bool isLeftMouse, Coords mouseGridPosition)
    {
        if (isLeftMouse) _pointerRegistration.LeftClickPosition = new Coords { Xi = mouseGridPosition.Xi, Yi =  mouseGridPosition.Yi};
        if (!isLeftMouse) _pointerRegistration.RightClickPosition = new Coords {Xi = mouseGridPosition.Xi, Yi = mouseGridPosition.Yi };

        if (!_pointerRegistration.IsValid()) return;

        Coords left = _pointerRegistration.LeftClickPosition ?? new();
        Coords right = _pointerRegistration.RightClickPosition ?? new();
        
        _museum.GenerateTilePath(left, right);
        _pointerRegistration.Reset();
    }

    public void UpdateTile(Coords mouseGridPosition) => _museum.HandleMouseTileUpdate(mouseGridPosition);

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
    
    public void Subscribe(ISimulationObserver observer) => _observers.Add(observer);

    private void NotifyAttendeeUpdated() => _observers.ForEach(observer => observer.UpdateSimulation());
    
    private void NotifyBackgroundUpdate(object? state) => _observers.ForEach(observer => observer.UpdateBackground());

    private void NotifyDebugInfoUpdate(object? state) => _observers.ForEach(observer => observer.UpdateDebugInfo());
    
    private void NotifyStopped() => _observers.ForEach(observer => observer.StopSimulation());

    private void NotifyOpenShortcutMenu() => _observers.ForEach(observer => observer.OpenShortcutMenu());
}