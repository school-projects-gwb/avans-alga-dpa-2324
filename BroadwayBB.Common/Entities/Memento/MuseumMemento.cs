using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Common.Entities.Memento;

public class MuseumMemento
{
    public List<IAttendee> Attendees { get; }
    public List<ITile> Tiles { get; }

    public MuseumMemento(List<ITile> tiles, List<IAttendee> attendees)
    {
        Attendees = attendees;
        Tiles = tiles;
    }
}