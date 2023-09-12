using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Test.CommonTests.TileManagerTests;

public class TileManagerTests : TileTestBase
{
    private readonly TileManager _tileManager = new();
    private readonly int collisionTilePosX = 0, collisionTilePosY = 0;
    
    [Fact]
    void TileManager_TopLeftAllowedTilePositions_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorTestGrid();
        var edges = gridEdges["topLeft"];

        var result = _tileManager.GetAllowedRelativeTilePositions(edges.Item1, edges.Item2);
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

        var result = _tileManager.GetAllowedRelativeTilePositions(edges.Item1, edges.Item2);
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

        var result = _tileManager.GetAllowedRelativeTilePositions(edges.Item1, edges.Item2);
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
        
        var result = _tileManager.GetAllowedRelativeTilePositions(edges.Item1, edges.Item2);
        Assert.DoesNotContain(MovementDirection.East, result);
        Assert.DoesNotContain(MovementDirection.South, result);
        Assert.Contains(MovementDirection.North, result);
        Assert.Contains(MovementDirection.West, result);
    }

    [Fact]
    void TileManager_BlueTileCollision_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorGridWithGivenColor(collisionTilePosX, collisionTilePosY, new BlueTileColor());
        var collisionResult = _tileManager.HandleCollision(collisionTilePosX, collisionTilePosY);

        var targetCollisionTile =
            _tileManager.Tiles.Find(tile => tile.PosX == collisionTilePosX && tile.PosY == collisionTilePosY);

        var adjacentTileChangeCount =
            GetAdjacentTileColorChangedAmount(_tileManager.Tiles, collisionTilePosX, collisionTilePosY);  
        
        Assert.IsType<YellowTileColor>(targetCollisionTile.TileColorBehavior);
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
            new Tile(0, 0, new WhiteTileColor()), // Default color
            new Tile(0, 1, new RedTileColor()),     // Different color
            new Tile(1, 0, new BlueTileColor()),    // Different color
            new Tile(1, 1, new WhiteTileColor()), // Default color
        };

        var targetPosX = 0;
        var targetPosY = 0;

        // Calculate the expected count of changed adjacent tiles.
        int expectedCount = 2;

        // Call the function and assert the result.
        var result = GetAdjacentTileColorChangedAmount(tiles, targetPosX, targetPosY);
        Assert.Equal(expectedCount, result);
    }
    
    [Fact]
    void TileManager_GreyTileCollision_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorGridWithGivenColor(collisionTilePosX, collisionTilePosY, new GreyTileColor());
        
        var targetCollisionTile =
            _tileManager.Tiles.Find(tile => tile.PosX == collisionTilePosX && tile.PosY == collisionTilePosY);
        
        // First collision
        var collisionResult = _tileManager.HandleCollision(collisionTilePosX, collisionTilePosY);
        Assert.IsType<GreyTileColor>(targetCollisionTile.TileColorBehavior);
        
        // Second collision
        collisionResult = _tileManager.HandleCollision(collisionTilePosX, collisionTilePosY);
        Assert.IsType<GreyTileColor>(targetCollisionTile.TileColorBehavior);
        
        // Third collision
        collisionResult = _tileManager.HandleCollision(collisionTilePosX, collisionTilePosY);
        
        var adjacentTileChangeCount =
            GetAdjacentTileColorChangedAmount(_tileManager.Tiles, collisionTilePosX, collisionTilePosY);  
        
        Assert.IsType<RedTileColor>(targetCollisionTile.TileColorBehavior);
        Assert.False(collisionResult.ShouldCreateArtist);
        Assert.False(collisionResult.ShouldRemoveArtist);
        Assert.Equal(0, adjacentTileChangeCount);
    }
    
    [Fact]
    void TileManager_RedTileCollision_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorGridWithGivenColor(collisionTilePosX, collisionTilePosY, new RedTileColor());
        var collisionResult = _tileManager.HandleCollision(collisionTilePosX, collisionTilePosY);

        var targetCollisionTile =
            _tileManager.Tiles.Find(tile => tile.PosX == collisionTilePosX && tile.PosY == collisionTilePosY);

        var adjacentTileChangeCount =
            GetAdjacentTileColorChangedAmount(_tileManager.Tiles, collisionTilePosX, collisionTilePosY);  
        
        Assert.IsType<BlueTileColor>(targetCollisionTile.TileColorBehavior);
        Assert.False(collisionResult.ShouldCreateArtist);
        Assert.True(collisionResult.ShouldRemoveArtist);
        Assert.Equal(0, adjacentTileChangeCount);
    }
    
    [Fact]
    void TileManager_WhiteTileCollision_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorGridWithGivenColor(collisionTilePosX, collisionTilePosY, new WhiteTileColor());
        var collisionResult = _tileManager.HandleCollision(collisionTilePosX, collisionTilePosY);

        var targetCollisionTile =
            _tileManager.Tiles.Find(tile => tile.PosX == collisionTilePosX && tile.PosY == collisionTilePosY);

        var adjacentTileChangeCount =
            GetAdjacentTileColorChangedAmount(_tileManager.Tiles, collisionTilePosX, collisionTilePosY);  
        
        Assert.IsType<WhiteTileColor>(targetCollisionTile.TileColorBehavior);
        Assert.False(collisionResult.ShouldCreateArtist);
        Assert.False(collisionResult.ShouldRemoveArtist);
        Assert.Equal(0, adjacentTileChangeCount);
    }
    
    [Fact]
    void TileManager_YellowTileCollision_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorGridWithGivenColor(collisionTilePosX, collisionTilePosY, new YellowTileColor());
        
        var targetCollisionTile =
            _tileManager.Tiles.Find(tile => tile.PosX == collisionTilePosX && tile.PosY == collisionTilePosY);
        
        // First collision
        var collisionResult = _tileManager.HandleCollision(collisionTilePosX, collisionTilePosY);
        Assert.IsType<YellowTileColor>(targetCollisionTile.TileColorBehavior);
        Assert.True(collisionResult.ShouldCreateArtist);
        
        // Second collision
        collisionResult = _tileManager.HandleCollision(collisionTilePosX, collisionTilePosY);
        
        var adjacentTileChangeCount =
            GetAdjacentTileColorChangedAmount(_tileManager.Tiles, collisionTilePosX, collisionTilePosY);  
        
        Assert.IsType<GreyTileColor>(targetCollisionTile.TileColorBehavior);
        Assert.True(collisionResult.ShouldCreateArtist);
        Assert.False(collisionResult.ShouldRemoveArtist);
        Assert.Equal(0, adjacentTileChangeCount);
    }
}