namespace BroadwayBB.Simulation;

public interface ISimulationObserver
{
    public void UpdateSimulation();
    public void StopSimulation();
}