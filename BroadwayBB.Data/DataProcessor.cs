using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Museum;
using BroadwayBB.Common.Entities.Tiles;
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
        GridDTO gridDTO;
        ArtistsDTO artistsDTO;

        try
        {
            gridDTO = (GridDTO)_reader.ReadFile(gridFile);
        }
        catch (ArgumentException AEx)
        {
            throw new ArgumentException("GRIDFILE: " + AEx.Message);
        }
        catch (InvalidCastException)
        {
            throw new InvalidCastException("GRIDFILE: File does not contain griddata.");
        }

        try
        {
            artistsDTO = (ArtistsDTO)_reader.ReadFile(artistsFile);
        }
        catch (ArgumentException AEx)
        {
            throw new ArgumentException("ARTISTSFILE: " + AEx.Message);
        }
        catch (InvalidCastException)
        {
            throw new InvalidCastException("ARTISTSFILE: File does not contain artistsdata.");
        }

        if (gridDTO.Rows < 1 || gridDTO.Columns < 1)
        {
            throw new ArgumentOutOfRangeException("Unable to create grid with given values.");
        }

        NodeDTO nullNode = new NodeDTO(null, new Structs.Coords(0,0));

        for (int y = 0; y < gridDTO.Rows; y++)
        {
            for (int x = 0; x < gridDTO.Columns; x++)
            {
                NodeDTO tile = gridDTO.Nodes.FirstOrDefault(n => n.Coords.X == x && n.Coords.Y == y); 


                if (tile.Equals(default(NodeDTO)))
                {
                    nullNode.Coords = new Structs.Coords(x, y);

                    tile = nullNode;
                }

                tiles.Add(_tileFactory.Create((int)tile.Coords.Y, (int)tile.Coords.X, tile.Type.Tag));
            }
        }

        foreach (var artist in artistsDTO.Artists)
        {
            artists.Add(_attendeeFactory.Create(artist.Coords.X, artist.Coords.Y, artist.VelocityY, artist.VelocityX));
        }

        museum.SetData(tiles, artists);
        
        return museum;
    }
}