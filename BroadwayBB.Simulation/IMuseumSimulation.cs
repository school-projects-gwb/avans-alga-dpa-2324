using BroadwayBB.Common.Entities;

namespace BroadwayBB.Simulation;

public interface IMuseumSimulation
{
    public Museum Museum { get; }
    public void Start();

    public void Subscribe(ISimulationObserver observer);
}