using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Common.Entities.Memento;

public class MuseumMemento
{
    public List<IAttendee> Attendees { get; }
    public List<Tile> Tiles { get; }

    public MuseumMemento(List<IAttendee> attendees, List<Tile> tiles)
    {
        Attendees = new List<IAttendee>(attendees);
        Tiles = new List<Tile>(tiles);
    }
}