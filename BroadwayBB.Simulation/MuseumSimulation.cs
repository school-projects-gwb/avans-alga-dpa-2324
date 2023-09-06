using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Simulation.Commands;

namespace BroadwayBB.Simulation;

public class MuseumSimulation : IMuseumSimulation
{
    private readonly List<ISimulationObserver> _observers = new();
    private Timer _simulationTimer;
    private readonly int _simulationIntervalMilliseconds = 100;
    public Museum Museum { get; }
    
    public MuseumSimulation(Museum museum)
    {
        Museum = museum;
        InitializeSimulationTimer();
    }
    
    private void InitializeSimulationTimer()
    {
        _simulationTimer = new Timer(Simulate, null, 0, _simulationIntervalMilliseconds);
    }
    
    private void Simulate(object? state)
    {
        Museum.MoveAttendees();
        NotifyUpdated();
    }
    
    public void Start()
    {
        _simulationTimer.Change(0, _simulationIntervalMilliseconds);
    }

    public void Subscribe(ISimulationObserver observer) => _observers.Add(observer);

    private void NotifyUpdated() => _observers.ForEach(observer => observer.UpdateSimulation());

    private void NotifyStopped() => _observers.ForEach(observer => observer.StopSimulation());
    
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
}