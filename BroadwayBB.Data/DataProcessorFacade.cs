using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Museum;
using BroadwayBB.Common.Entities.Tiles;
using BroadwayBB.Data.DTOs;
using BroadwayBB.Data.Factories;
using BroadwayBB.Data.Factories.Interfaces;
using BroadwayBB.Data.Strategies;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Helpers;

namespace BroadwayBB.Data;

public class DataProcessorFacade
{
    private FileReader _reader;
    private ITileFactory _tileFactory;
    private IAttendeeFactory _attendeeFactory;

    public DataProcessorFacade()
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


        foreach (var nodeType in gridDTO.NodeTypes)
        {
            ColorRegistryHelper.GetInstance.RegisterColor(nodeType.ColorName, new RgbColor(nodeType.Color));
            WeightRegistryHelper.GetInstance.RegisterWeight(nodeType.ColorName, nodeType.Weight);
        }

        for (int y = 0; y < gridDTO.Rows; y++)
        {
            for (int x = 0; x < gridDTO.Columns; x++)
            {
                NodeDTO tile = gridDTO.Nodes.FirstOrDefault(n => n.Coords.Xi == x && n.Coords.Yi == y); 

                if (tile.Equals(default(NodeDTO)))
                {
                    tile.Coords.Xd = x;
                    tile.Coords.Yd = y;
                }

                tiles.Add(_tileFactory.Create(tile.Type.ColorName, tile.Coords));
            }
        }

        foreach (var artist in artistsDTO.Artists)
        {
            var coords = artist.Coords;

            if (coords.Xd > gridDTO.Columns || coords.Xd < 0) coords.Xd = 0;
            if (coords.Yd > gridDTO.Rows    || coords.Yd < 0) coords.Yd = 0;

            artists.Add(_attendeeFactory.Create(coords, artist.VelocityY, artist.VelocityX));
        }

        museum.SetData(tiles, artists);
        
        return museum;
    }
}