namespace BroadwayBB.Simulation;

public interface ISimulationObserver
{
    public void UpdateSimulation();
    public void UpdateBackground();
    public void StopSimulation();
    public void OpenShortcutMenu();
    void UpdateDebugInfo();
}