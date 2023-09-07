using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Simulation.Commands;

namespace BroadwayBB.Simulation;

public class MuseumSimulation : IMuseumSimulation
{
    private readonly List<ISimulationObserver> _observers = new();
    private Timer _simulationTimer;
    private Timer _simulationBackgroundTimer;
    private readonly int _simulationIntervalMilliseconds = 175;
    private readonly int _backgroundUpdateIntervalMilliseconds = 500;
    public Museum Museum { get; }
    
    public MuseumSimulation(Museum museum)
    {
        Museum = museum;
        InitializeSimulationTimers();
    }
    
    private void InitializeSimulationTimers()
    {
        _simulationTimer = new Timer(Simulate, null, 0, _simulationIntervalMilliseconds);
        _simulationBackgroundTimer = new Timer(NotifyBackgroundUpdate, null, 0, _backgroundUpdateIntervalMilliseconds);
    }
    
    private void Simulate(object? state)
    {
        Museum.MoveAttendees();
        NotifyAttendeeUpdated();
    }
    
    public void Start()
    {
        _simulationTimer.Change(0, _simulationIntervalMilliseconds);
        _simulationBackgroundTimer.Change(0, _backgroundUpdateIntervalMilliseconds);
    }
    
    public void ToggleAttendeeMovement()
    {
        Museum.MuseumConfiguration.ShouldMoveAttendees = !Museum.MuseumConfiguration.ShouldMoveAttendees;
    }

    public void ToggleAttendeeRendering()
    {
        Museum.MuseumConfiguration.ShouldRenderAttendees = !Museum.MuseumConfiguration.ShouldRenderAttendees;
    }

    public void UpdateTile(MouseGridPosition mouseGridPosition)
    {
        Museum.HandleMouseTileUpdate(mouseGridPosition.PosX, mouseGridPosition.PosY);
    }

    public void OpenFileMenu() => NotifyStopped();

    public void OpenShortcutMenu() => NotifyOpenShortcutMenu();
    
    public void Subscribe(ISimulationObserver observer) => _observers.Add(observer);

    private void NotifyAttendeeUpdated() => _observers.ForEach(observer => observer.UpdateSimulation());
    private void NotifyBackgroundUpdate(object? state) => _observers.ForEach(observer => observer.UpdateBackground());

    private void NotifyStopped() => _observers.ForEach(observer => observer.StopSimulation());

    private void NotifyOpenShortcutMenu() => _observers.ForEach(observer => observer.OpenShortcutMenu());
}