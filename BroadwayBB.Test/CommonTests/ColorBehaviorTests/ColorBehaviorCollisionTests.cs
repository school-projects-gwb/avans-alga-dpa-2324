using BroadwayBB.Common.Behaviors;

namespace BroadwayBB.Test.CommonTests.ColorBehaviorTests;

public class ColorBehaviorCollisionTests
{
    [Fact]
    public void BlueColor_OnCollision_ResultCorrect()
    {
        var tileColor = new BlueColorBehaviorStrategy();
        var result = tileColor.HandleCollision();

        Assert.IsType<YellowColorBehaviorStrategy>(result.UpdatedCollisionTargetColor);
        Assert.Equal(2, result.UpdatedAdjacentTileColors.Count);
        Assert.True(result.ShouldCreateArtist);
        Assert.False(result.ShouldRemoveArtist);
    }
    
    [Fact]
    public void GreyColor_OnCollision_ResultCorrect()
    {
        var tileColor = new GreyColorBehaviorStrategy();
        
        // Collision 1
        var result = tileColor.HandleCollision();
        Assert.IsType<GreyColorBehaviorStrategy>(result.UpdatedCollisionTargetColor);
        
        // Collision 2
        result = tileColor.HandleCollision();
        Assert.IsType<GreyColorBehaviorStrategy>(result.UpdatedCollisionTargetColor);

        // Collision 3
        result = tileColor.HandleCollision();
        
        Assert.IsType<RedColorBehaviorStrategy>(result.UpdatedCollisionTargetColor);
        Assert.Empty(result.UpdatedAdjacentTileColors);
        Assert.False(result.ShouldCreateArtist);
        Assert.False(result.ShouldRemoveArtist);
    }
    
    [Fact]
    public void RedColor_OnCollision_ResultCorrect()
    {
        var tileColor = new RedColorBehaviorStrategy();
        var result = tileColor.HandleCollision();
        
        Assert.IsType<BlueColorBehaviorStrategy>(result.UpdatedCollisionTargetColor);
        Assert.Empty(result.UpdatedAdjacentTileColors);
        Assert.False(result.ShouldCreateArtist);
        Assert.True(result.ShouldRemoveArtist);
    }
    
    [Fact]
    public void WhiteColor_OnCollision_ResultCorrect()
    {
        var tileColor = new WhiteColorBehaviorStrategy();
        var result = tileColor.HandleCollision();
        
        Assert.IsType<WhiteColorBehaviorStrategy>(result.UpdatedCollisionTargetColor);
        Assert.Empty(result.UpdatedAdjacentTileColors);
        Assert.False(result.ShouldCreateArtist);
        Assert.False(result.ShouldRemoveArtist);
    }
    
    [Fact]
    public void YellowColor_OnCollision_ResultCorrect()
    {
        var tileColor = new YellowColorBehaviorStrategy();
        
        // Collision 1
        var result = tileColor.HandleCollision();
        Assert.IsType<YellowColorBehaviorStrategy>(result.UpdatedCollisionTargetColor);
        Assert.True(result.ShouldCreateArtist);
        
        // Collision 2
        result = tileColor.HandleCollision();
        
        Assert.IsType<GreyColorBehaviorStrategy>(result.UpdatedCollisionTargetColor);
        Assert.Empty(result.UpdatedAdjacentTileColors);
        Assert.True(result.ShouldCreateArtist);
        Assert.False(result.ShouldRemoveArtist);
    }
}