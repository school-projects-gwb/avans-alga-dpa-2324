using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Common.Entities.Memento;

public class MuseumMemento
{
    public List<IAttendee> Attendees { get; }
    public List<TileNode> TileNodes { get; }

    public MuseumMemento(List<TileNode> tileNodes, List<IAttendee> attendees)
    {
        TileNodes = tileNodes;
        Attendees = attendees;
    }
}