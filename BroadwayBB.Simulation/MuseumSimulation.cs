using BroadwayBB.Common.Entities;

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
        NotifySubscribers();
    }
    
    public void Start()
    {
        _simulationTimer.Change(0, _simulationIntervalMilliseconds);
    }

    public void Subscribe(ISimulationObserver observer) => _observers.Add(observer);

    private void NotifySubscribers() => _observers.ForEach(observer => observer.UpdateSimulation());

    public void ToggleAttendeeMovement()
    {
        Museum.MuseumConfiguration.ShouldMoveAttendees = !Museum.MuseumConfiguration.ShouldMoveAttendees;
    }

    public void ToggleAttendeeRendering()
    {
        Museum.MuseumConfiguration.ShouldRenderAttendees = !Museum.MuseumConfiguration.ShouldRenderAttendees;
    }
    
}