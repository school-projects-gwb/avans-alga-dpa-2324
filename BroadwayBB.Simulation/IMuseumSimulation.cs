using System.Drawing;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation;

public interface IMuseumSimulation
{
    public void Subscribe(ISimulationObserver observer);
    public List<ITile> GetMuseumTiles();
    public List<IAttendee> GetMuseumAttendees();
    public List<Rectangle> GetDebugInfo();
    public int GetMaxMuseumAttendees();
    public void ToggleAttendeeMovement();
    public void ToggleQuadtreeRendering();
    public void ToggleAttendeeRendering();
    public void UpdateTile(MouseGridPosition mouseGridPosition);
    public void OpenFileMenu();
    public void FastForward();
    public void Rewind();
    public void OpenShortcutMenu();
}