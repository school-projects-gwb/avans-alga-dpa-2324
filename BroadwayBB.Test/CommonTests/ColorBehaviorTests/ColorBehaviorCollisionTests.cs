using BroadwayBB.Common.Behaviors;

namespace BroadwayBB.Test.CommonTests.ColorBehaviorTests;

public class ColorBehaviorCollisionTests
{
    [Fact]
    public void BlueColor_OnCollision_ResultCorrect()
    {
        var tileColor = new BlueTileColor();
        var result = tileColor.HandleCollision();

        Assert.IsType<YellowTileColor>(result.UpdatedCollisionTargetTileColor);
        Assert.Equal(2, result.UpdatedAdjacentTileColors.Count);
        Assert.True(result.ShouldCreateArtist);
        Assert.False(result.ShouldRemoveArtist);
    }
    
    [Fact]
    public void GreyColor_OnCollision_ResultCorrect()
    {
        var tileColor = new GreyTileColor();
        
        // Collision 1
        var result = tileColor.HandleCollision();
        Assert.IsType<GreyTileColor>(result.UpdatedCollisionTargetTileColor);
        
        // Collision 2
        result = tileColor.HandleCollision();
        Assert.IsType<GreyTileColor>(result.UpdatedCollisionTargetTileColor);

        // Collision 3
        result = tileColor.HandleCollision();
        
        Assert.IsType<RedTileColor>(result.UpdatedCollisionTargetTileColor);
        Assert.Empty(result.UpdatedAdjacentTileColors);
        Assert.False(result.ShouldCreateArtist);
        Assert.False(result.ShouldRemoveArtist);
    }
    
    [Fact]
    public void RedColor_OnCollision_ResultCorrect()
    {
        var tileColor = new RedTileColor();
        var result = tileColor.HandleCollision();
        
        Assert.IsType<BlueTileColor>(result.UpdatedCollisionTargetTileColor);
        Assert.Empty(result.UpdatedAdjacentTileColors);
        Assert.False(result.ShouldCreateArtist);
        Assert.True(result.ShouldRemoveArtist);
    }
    
    [Fact]
    public void WhiteColor_OnCollision_ResultCorrect()
    {
        var tileColor = new WhiteTileColor();
        var result = tileColor.HandleCollision();
        
        Assert.IsType<WhiteTileColor>(result.UpdatedCollisionTargetTileColor);
        Assert.Empty(result.UpdatedAdjacentTileColors);
        Assert.False(result.ShouldCreateArtist);
        Assert.False(result.ShouldRemoveArtist);
    }
    
    [Fact]
    public void YellowColor_OnCollision_ResultCorrect()
    {
        var tileColor = new YellowTileColor();
        
        // Collision 1
        var result = tileColor.HandleCollision();
        Assert.IsType<YellowTileColor>(result.UpdatedCollisionTargetTileColor);
        Assert.True(result.ShouldCreateArtist);
        
        // Collision 2
        result = tileColor.HandleCollision();
        
        Assert.IsType<GreyTileColor>(result.UpdatedCollisionTargetTileColor);
        Assert.Empty(result.UpdatedAdjacentTileColors);
        Assert.True(result.ShouldCreateArtist);
        Assert.False(result.ShouldRemoveArtist);
    }
}