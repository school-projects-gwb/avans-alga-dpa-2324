using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Simulation.Commands;

namespace BroadwayBB.Simulation;

public interface IMuseumSimulation
{
    public Museum Museum { get; }
    public void Start();

    public void Subscribe(ISimulationObserver observer);
    public void ToggleAttendeeMovement();
    public void ToggleAttendeeRendering();
    public void UpdateTile(MouseGridPosition mouseGridPosition);
}