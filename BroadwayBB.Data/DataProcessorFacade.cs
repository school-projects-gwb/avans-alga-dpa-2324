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
    private const int TileLimit = 6400; // 80 by 80
    private readonly FileReader _reader = new();
    private readonly IColorBehaviorStrategyFactory _colorBehaviorStrategyFactory = new ColorBehaviorStrategyFactory();
    private readonly IAttendeeFactory _attendeeFactory = new AttendeeFactory();

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
        else if ((gridDTO.Rows * gridDTO.Columns) > TileLimit)
        {
            throw new ArgumentOutOfRangeException("Unable to create grid; grid is too big.");
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
                
                ITile newTile = new Tile(tile.Coords, _colorBehaviorStrategyFactory.Create(tile.Type.ColorName));
                tiles.Add(newTile);
            }
        }

        foreach (var artist in artistsDTO.Artists)
        {
            var coords = artist.Coords;
            var velY = artist.VelocityY;

            if (coords.Xd > gridDTO.Columns || coords.Xd < 0) coords.Xd = 0;
            if (coords.Yd > gridDTO.Rows || coords.Yd < 0) coords.Yd = 0;
            if (artist.VelocityX != 0 && velY != 0) velY = 0.0;

            artists.Add(_attendeeFactory.Create(coords, velY, artist.VelocityX));
        }

        museum.SetData(tiles, artists);
        
        return museum;
    }
}