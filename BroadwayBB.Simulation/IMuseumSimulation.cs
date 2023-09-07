using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Simulation.Commands;

namespace BroadwayBB.Simulation;

public interface IMuseumSimulation
{
    public void Subscribe(ISimulationObserver observer);
    public List<ITile> GetMuseumTiles();
    public List<IAttendee> GetMuseumAttendees();
    public int GetMaxMuseumAttendees();
    public void ToggleAttendeeMovement();
    public void ToggleAttendeeRendering();
    public void UpdateTile(MouseGridPosition mouseGridPosition);
    public void OpenFileMenu();
    public void FastForward();
    public void OpenShortcutMenu();
}