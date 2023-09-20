using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Data.DTOs;
using BroadwayBB.Data.Factories;
using BroadwayBB.Data.Factories.Interfaces;
using BroadwayBB.Data.Strategies;

namespace BroadwayBB.Data;

public class DataProcessor
{
    private FileReader _reader;
    private ITileFactory _tileFactory;
    private IAttendeeFactory _attendeeFactory;

    public DataProcessor()
    {
        _reader = new FileReader();
        _tileFactory = new TileFactory();
        _attendeeFactory = new AttendeeFactory();
    }

    public Museum BuildMuseumFromFiles(string gridFile, string artistsFile)
    {
        Museum museum = new Museum();
        List<ITile> tiles = new List<ITile>();
        List<IAttendee> artists = new List<IAttendee>();

        GridDTO gridDTO = (GridDTO?)_reader.ReadFile(gridFile) ?? new GridDTO();
        ArtistsDTO artistsDTO = (ArtistsDTO?)_reader.ReadFile(artistsFile) ?? new ArtistsDTO();
       

        char[] colors = { 'R', 'B', '_', 'Y', 'G' };
        NodeDTO nullNode = new NodeDTO(null, new Structs.Coords(0,0));

        for (int y = 0; y < gridDTO.Rows; y++)
        {
            for (int x = 0; x < gridDTO.Columns; x++)
            {
                NodeDTO tile = gridDTO.Nodes.Find(n => n.Coords.X == x && n.Coords.Y == y); 

                if (tile.Edges == null && tile.Type.Tag == '\0')
                {
                    nullNode.Coords = new Structs.Coords(x, y);

                    tile = nullNode;
                }


                tiles.Add(_tileFactory.Create((int)tile.Coords.Y, (int)tile.Coords.X, tile.Type.Tag));
            }
        }

        if (artistsDTO.Artists.Count > 0) {
            foreach (var artist in artistsDTO.Artists)
            {
                artists.Add(_attendeeFactory.Create(artist.Coords.X, artist.Coords.Y, artist.VelocityY, artist.VelocityX));
            }
        }
        else
        {
            artists.Add(_attendeeFactory.Create(2.5, 3, 0, 2.5));
            artists.Add(_attendeeFactory.Create(2, 2, 2, 0));
            artists.Add(_attendeeFactory.Create(1, 3, 0, 2));
            artists.Add(_attendeeFactory.Create(2.5, 3, 0, 1));
        }
        
        museum.Attendees = artists;
        museum.Tiles = tiles;
        
        return museum;
    }
}