using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Data.Factories;
using BroadwayBB.Data.Factories.Interfaces;

namespace BroadwayBB.Data;

public class DataProcessor
{
    public Museum BuildMuseumFromFile(string filePath)
    {
        Museum museum = new Museum();
        List<IAttendee> artists = new List<IAttendee>();

        ITileFactory tileFactory = new TileFactory();
        IAttendeeFactory attendeeFactory = new AttendeeFactory();

        char[] colors = { 'R', 'B', '_', 'Y', 'G' };
        var random = new Random();
        int colRowAmount = 60;

        List<ITile> tiles = new();

        for (int y = 0; y < colRowAmount; y++)
        {
            for (int x = 0; x < colRowAmount; x++)
            {
                var tile = tileFactory.Create(y, x, colors[random.Next(colors.Length)]);
                tiles.Add(tile);
            }
        }
        
        artists.Add(attendeeFactory.Create(2.5, 3, 0, 2.5));
        artists.Add(attendeeFactory.Create(2, 2, 2, 0));
        artists.Add(attendeeFactory.Create(1, 3, 0, 2));
        artists.Add(attendeeFactory.Create(2.5, 3, 0, 1));
        
        museum.Attendees = artists;
        museum.Tiles = tiles;
        
        return museum;
    }
}