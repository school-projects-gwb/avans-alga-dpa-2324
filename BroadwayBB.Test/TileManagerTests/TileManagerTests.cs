using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities;

namespace BroadwayBB.Test.TileManagerTests;

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
        _tileManager.Tiles = CreateWhiteColorGridWithTopLeftColor(collisionTilePosX, collisionTilePosY, new BlueTileColor());
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
    void TileManager_GreyTileCollision_ResultCorrect()
    {
        _tileManager.Tiles = CreateWhiteColorGridWithTopLeftColor(collisionTilePosX, collisionTilePosY, new GreyTileColor());
        
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
        _tileManager.Tiles = CreateWhiteColorGridWithTopLeftColor(collisionTilePosX, collisionTilePosY, new RedTileColor());
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
        _tileManager.Tiles = CreateWhiteColorGridWithTopLeftColor(collisionTilePosX, collisionTilePosY, new WhiteTileColor());
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
        _tileManager.Tiles = CreateWhiteColorGridWithTopLeftColor(collisionTilePosX, collisionTilePosY, new YellowTileColor());
        
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