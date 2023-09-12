using BroadwayBB.Common.Entities;

namespace BroadwayBB.Test.CommonTests.TileManagerTests;

public class TileManagerDeepCopyTests : TileTestBase
{
    [Fact]
    void TileManager_CreateMemento_DifferentObjects()
    {
        var tileManager = new TileManager();
        tileManager.Tiles = CreateWhiteColorTestGrid();

        var tileCopy = tileManager.CreateMemento();
        
        Assert.NotSame(tileManager.Tiles, tileCopy);
    }
}