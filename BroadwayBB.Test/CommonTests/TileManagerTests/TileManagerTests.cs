using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Test.CommonTests.TileManagerTests;

public class TileManagerTests : TileTestBase
{
    private readonly TileManager _tileManager = new();
    private readonly Coords collisionTilePos = new(0,0);
    
    [Fact]
    void TileManager_TopLeftAllowedTilePositions_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorTestGrid();
        var edges = gridEdges["topLeft"];

        var result = _tileManager.GetAllowedRelativeTilePositions(edges);
        Assert.Contains(MovementDirection.East, result);
        Assert.Contains(MovementDirection.South, result);
        Assert.DoesNotContain(MovementDirection.North, result);
        Assert.DoesNotContain(MovementDirection.West, result);
    }
    
    [Fact]
    void TileManager_TopRightAllowedTilePositions_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorTestGrid();
        var edges = gridEdges["topRight"];

        var result = _tileManager.GetAllowedRelativeTilePositions(edges);
        Assert.DoesNotContain(MovementDirection.East, result);
        Assert.Contains(MovementDirection.South, result);
        Assert.DoesNotContain(MovementDirection.North, result);
        Assert.Contains(MovementDirection.West, result);
    }
    
    [Fact]
    void TileManager_BottomLeftAllowedTilePositions_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorTestGrid();
        var edges = gridEdges["bottomLeft"];

        var result = _tileManager.GetAllowedRelativeTilePositions(edges);
        Assert.Contains(MovementDirection.East, result);
        Assert.DoesNotContain(MovementDirection.South, result);
        Assert.Contains(MovementDirection.North, result);
        Assert.DoesNotContain(MovementDirection.West, result);
    }
    
    [Fact]
    void TileManager_BottomRightAllowedTilePositions_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorTestGrid();
        var edges = gridEdges["bottomRight"];
        
        var result = _tileManager.GetAllowedRelativeTilePositions(edges);
        Assert.DoesNotContain(MovementDirection.East, result);
        Assert.DoesNotContain(MovementDirection.South, result);
        Assert.Contains(MovementDirection.North, result);
        Assert.Contains(MovementDirection.West, result);
    }

    [Fact]
    void TileManager_BlueTileCollision_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorGridWithGivenColor(collisionTilePos, new BlueColorBehaviorStrategy());
        var collisionResult = _tileManager.HandleCollision(collisionTilePos, true);

        var targetCollisionTile =
            _tileManager.Tiles.Find(tile => Coords.IntEqual(tile.Pos, collisionTilePos));

        var adjacentTileChangeCount =
            GetAdjacentTileColorChangedAmount(_tileManager.Tiles, collisionTilePos);  
        
        Assert.IsType<YellowColorBehaviorStrategy>(targetCollisionTile.ColorBehaviorStrategy);
        Assert.True(collisionResult.ShouldCreateArtist);
        Assert.False(collisionResult.ShouldRemoveArtist);
        Assert.Equal(2, adjacentTileChangeCount);
    }
    
    [Fact]
    public void GetAdjacentTileColorChangedAmount_ReturnsCorrectCount()
    {
        // Create a list of test tiles with different colors.
        var tiles = new List<ITile>
        {
            new Tile(new Coords(0, 0), new NullColorBehaviorStrategy()), // Default color
            new Tile(new Coords(0, 1), new RedColorBehaviorStrategy()),  // Different color
            new Tile(new Coords(1, 0), new BlueColorBehaviorStrategy()), // Different color
            new Tile(new Coords(1, 1), new NullColorBehaviorStrategy()), // Default color
        };

        var targetPos = new Coords(0, 0);

        // Calculate the expected count of changed adjacent tiles.
        int expectedCount = 2;

        // Call the function and assert the result.
        var result = GetAdjacentTileColorChangedAmount(tiles, targetPos);
        Assert.Equal(expectedCount, result);
    }
    
    [Fact]
    void TileManager_GreyTileCollision_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorGridWithGivenColor(collisionTilePos, new GreyColorBehaviorStrategy());
        
        var targetCollisionTile =
            _tileManager.Tiles.Find(tile => Coords.IntEqual(tile.Pos, collisionTilePos));
        
        // First collision
        var collisionResult = _tileManager.HandleCollision(collisionTilePos, true);
        Assert.IsType<GreyColorBehaviorStrategy>(targetCollisionTile?.ColorBehaviorStrategy);
        
        // Second collision
        collisionResult = _tileManager.HandleCollision(collisionTilePos, true);
        Assert.IsType<GreyColorBehaviorStrategy>(targetCollisionTile.ColorBehaviorStrategy);
        
        // Third collision
        collisionResult = _tileManager.HandleCollision(collisionTilePos, true);
        
        var adjacentTileChangeCount =
            GetAdjacentTileColorChangedAmount(_tileManager.Tiles, collisionTilePos);  
        
        Assert.IsType<RedColorBehaviorStrategy>(targetCollisionTile.ColorBehaviorStrategy);
        Assert.False(collisionResult.ShouldCreateArtist);
        Assert.False(collisionResult.ShouldRemoveArtist);
        Assert.Equal(0, adjacentTileChangeCount);
    }
    
    [Fact]
    void TileManager_RedTileCollision_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorGridWithGivenColor(collisionTilePos, new RedColorBehaviorStrategy());
        var collisionResult = _tileManager.HandleCollision(collisionTilePos, true);

        var targetCollisionTile =
            _tileManager.Tiles.Find(tile => Coords.IntEqual(tile.Pos, collisionTilePos));

        var adjacentTileChangeCount =
            GetAdjacentTileColorChangedAmount(_tileManager.Tiles, collisionTilePos);  
        
        Assert.IsType<BlueColorBehaviorStrategy>(targetCollisionTile?.ColorBehaviorStrategy);
        Assert.False(collisionResult.ShouldCreateArtist);
        Assert.True(collisionResult.ShouldRemoveArtist);
        Assert.Equal(0, adjacentTileChangeCount);
    }
    
    [Fact]
    void TileManager_WhiteTileCollision_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorGridWithGivenColor(collisionTilePos, new NullColorBehaviorStrategy());
        var collisionResult = _tileManager.HandleCollision(collisionTilePos, true);

        var targetCollisionTile =
            _tileManager.Tiles.Find(tile => Coords.IntEqual(tile.Pos, collisionTilePos));

        var adjacentTileChangeCount =
            GetAdjacentTileColorChangedAmount(_tileManager.Tiles, collisionTilePos);  
        
        Assert.IsType<NullColorBehaviorStrategy>(targetCollisionTile?.ColorBehaviorStrategy);
        Assert.False(collisionResult.ShouldCreateArtist);
        Assert.False(collisionResult.ShouldRemoveArtist);
        Assert.Equal(0, adjacentTileChangeCount);
    }
    
    [Fact]
    void TileManager_YellowTileCollision_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorGridWithGivenColor(collisionTilePos, new YellowColorBehaviorStrategy());
        
        var targetCollisionTile =
            _tileManager.Tiles.Find(tile => Coords.IntEqual(tile.Pos, collisionTilePos));
        
        // First collision
        var collisionResult = _tileManager.HandleCollision(collisionTilePos, true);
        Assert.IsType<YellowColorBehaviorStrategy>(targetCollisionTile?.ColorBehaviorStrategy);
        Assert.True(collisionResult.ShouldCreateArtist);
        
        // Second collision
        collisionResult = _tileManager.HandleCollision(collisionTilePos, true);
        
        var adjacentTileChangeCount =
            GetAdjacentTileColorChangedAmount(_tileManager.Tiles, collisionTilePos);  
        
        Assert.IsType<GreyColorBehaviorStrategy>(targetCollisionTile?.ColorBehaviorStrategy);
        Assert.True(collisionResult.ShouldCreateArtist);
        Assert.False(collisionResult.ShouldRemoveArtist);
        Assert.Equal(0, adjacentTileChangeCount);
    }
}