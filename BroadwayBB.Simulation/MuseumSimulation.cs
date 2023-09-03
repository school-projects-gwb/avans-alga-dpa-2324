using BroadwayBB.Common.Models;

namespace BroadwayBB.Simulation;

public class MuseumSimulation
{
    private readonly List<ISimulationObserver> _observers = new List<ISimulationObserver>();
    private Timer _simulationTimer;
    private readonly int _simulationIntervalMilliseconds = 100;
    public Museum Museum { get; private set; }
    
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
}