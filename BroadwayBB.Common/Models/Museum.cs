using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models;

public class Museum
{
    public List<ITile> Tiles { get; private set; }
    public List<Artist> Artists { get; private set; }
    
    public void SetTiles(List<ITile> tiles) => Tiles = tiles;

    public void SetArtists(List<Artist> artists) => Artists = artists;
}