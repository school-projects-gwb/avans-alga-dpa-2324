using BroadwayBB.Common.Models;

namespace BroadwayBB.Simulation;

public class MuseumSimulation
{
    private List<ISimulationObserver> _observers = new List<ISimulationObserver>();
    public Museum Museum { get; private set; }
    
    public MuseumSimulation(Museum museum)
    {
        Museum = museum;
    }

    public void Subscribe(ISimulationObserver observer) => _observers.Add(observer);

    private void NotifySubscribers() => _observers.ForEach(observer => observer.UpdateSimulation());
}