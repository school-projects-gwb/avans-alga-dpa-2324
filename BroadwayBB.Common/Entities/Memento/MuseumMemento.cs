using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Memento;

public class MuseumMemento
{
    public List<IAttendee> Attendees { get; }
    public List<ITile> Tiles { get; }

    public MuseumMemento(List<ITile> tiles, List<IAttendee> attendees)
    {
        Tiles = tiles;
        Attendees = attendees;
    }
}