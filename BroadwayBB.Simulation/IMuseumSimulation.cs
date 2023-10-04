using System.Drawing;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Museum;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Simulation;

public interface IMuseumSimulation
{
    public void Subscribe(ISimulationObserver observer);
    public List<ITile> GetMuseumTiles();
    public List<IAttendee> GetMuseumAttendees();
    public List<DebugTile> GetDebugInfo();
    public int GetMaxMuseumAttendees();
    public void ToggleConfigValue(ConfigType type);
    public void HandlePointerClick(bool isLeftMouse, Coords mouseGridPosition);
    public void UpdateTile(Coords mouseGridPosition);
    public void OpenFileMenu();
    public void FastForward();
    public void Rewind();
    public void OpenShortcutMenu();
}