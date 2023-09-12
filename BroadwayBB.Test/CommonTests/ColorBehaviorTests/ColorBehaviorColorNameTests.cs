using BroadwayBB.Common.Behaviors;

namespace BroadwayBB.Test.CommonTests.ColorBehaviorTests;

public class ColorBehaviorColorNameTests
{
    [Fact]
    public void BlueColor_ColorName_Correct()
    {
        var tileColor = new BlueTileColor();
        Assert.Equal(ColorName.Blue, tileColor.ColorName);
    }
    
    [Fact]
    public void GreyColor_ColorName_Correct()
    {
        var tileColor = new GreyTileColor();
        Assert.Equal(ColorName.Grey, tileColor.ColorName);
    }
    
    [Fact]
    public void WhiteColor_ColorName_Correct()
    {
        var tileColor = new WhiteTileColor();
        Assert.Equal(ColorName.White, tileColor.ColorName);
    }
    
    [Fact]
    public void RedColor_ColorName_Correct()
    {
        var tileColor = new RedTileColor();
        Assert.Equal(ColorName.Red, tileColor.ColorName);
    }
    
    [Fact]
    public void YellowColor_ColorName_Correct()
    {
        var tileColor = new YellowTileColor();
        Assert.Equal(ColorName.Yellow, tileColor.ColorName);
    }
}