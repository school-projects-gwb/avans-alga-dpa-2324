using BroadwayBB.Common.Entities;

namespace BroadwayBB.Test.CommonTests.ArtistTests;

public class ArtistMovementTests : TileTestBase
{
    void Test()
    {
        var artist = new Artist(0,0, 2, 0);
        
        var tileManager = new TileManager();
        tileManager.Tiles = CreateWhiteColorTestGrid();
    }
    
    
}