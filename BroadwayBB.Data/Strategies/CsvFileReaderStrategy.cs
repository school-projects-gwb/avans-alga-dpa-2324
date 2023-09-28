using BroadwayBB.Data.DTOs;
using BroadwayBB.Data.Strategies.Interfaces;
using BroadwayBB.Data.Structs;

namespace BroadwayBB.Data.Strategies;

public class CsvFileReaderStrategy : IFileReaderStrategy
{
    public DTO ReadFile(FileData file)
    {
        var artists = new ArtistsDTO();

        var lines = file.Data.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        var header = lines[0].Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();

        for (int i = 1; i < lines.Length; i++) {
            var line = lines[i].Split(",", StringSplitOptions.RemoveEmptyEntries);

            var x = line[header.IndexOf("x")];
            var y = line[header.IndexOf("y")];
            var vx = line[header.IndexOf("vx")];
            var vy = line[header.IndexOf("vy")];

            artists.Artists.Add(new ArtistDTO(x, y, vx, vy));
        }

        return artists;
    }
}