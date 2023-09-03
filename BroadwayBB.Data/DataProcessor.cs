using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Models;
using BroadwayBB.Common.Models.Interfaces;
using BroadwayBB.Data.Factories;
using BroadwayBB.Data.Factories.Interfaces;

namespace BroadwayBB.Data;

public class DataProcessor
{
    public Museum BuildMuseumFromFile(string filePath)
    {
        Museum museum = new Museum();
        List<ITile> tiles = new List<ITile>();
        List<IAttendee> artists = new List<IAttendee>();

        ITileFactory tileFactory = new TileFactory();
        IAttendeeFactory attendeeFactory = new AttendeeFactory();
        
        tiles.Add(tileFactory.Create(0, 0, 'R'));
        tiles.Add(tileFactory.Create(1, 0, 'R'));
        tiles.Add(tileFactory.Create(2, 0, 'R'));
        tiles.Add(tileFactory.Create(3, 0, 'R'));
        tiles.Add(tileFactory.Create(4, 0, 'R'));
        
        tiles.Add(tileFactory.Create(0, 1, 'B'));
        tiles.Add(tileFactory.Create(1, 1, 'B'));
        tiles.Add(tileFactory.Create(2, 1, 'B'));
        tiles.Add(tileFactory.Create(3, 1, 'B'));
        tiles.Add(tileFactory.Create(4, 1, 'B'));
        
        tiles.Add(tileFactory.Create(0, 2, '_'));
        tiles.Add(tileFactory.Create(1, 2, '_'));
        tiles.Add(tileFactory.Create(2, 2, '_'));
        tiles.Add(tileFactory.Create(3, 2, '_'));
        tiles.Add(tileFactory.Create(4, 2, '_'));
        
        tiles.Add(tileFactory.Create(0, 3, 'Y'));
        tiles.Add(tileFactory.Create(1, 3, 'Y'));
        tiles.Add(tileFactory.Create(2, 3, 'Y'));
        tiles.Add(tileFactory.Create(3, 3, 'Y'));
        tiles.Add(tileFactory.Create(4, 3, 'Y'));
        
        tiles.Add(tileFactory.Create(0, 4, 'G'));
        tiles.Add(tileFactory.Create(1, 4, 'G'));
        tiles.Add(tileFactory.Create(2, 4, 'G'));
        tiles.Add(tileFactory.Create(3, 4, 'G'));
        tiles.Add(tileFactory.Create(4, 4, 'G'));

        artists.Add(attendeeFactory.Create(1.5, 2, 2, 0));
        artists.Add(attendeeFactory.Create(3, 2.5, 2, 0));
        artists.Add(attendeeFactory.Create(2.5, 3, 2, 0));
        
        museum.SetArtists(artists);
        museum.SetTiles(tiles);
        
        return museum;
    }
}