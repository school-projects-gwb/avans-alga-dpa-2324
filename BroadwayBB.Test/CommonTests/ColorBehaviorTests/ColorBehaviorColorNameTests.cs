using BroadwayBB.Common.Behaviors;

namespace BroadwayBB.Test.CommonTests.ColorBehaviorTests;

public class ColorBehaviorColorNameTests
{
    [Fact]
    public void BlueColor_ColorName_Correct()
    {
        var tileColor = new BlueColorBehaviorStrategy();
        Assert.Equal(ColorName.Blue, tileColor.ColorName);
    }
    
    [Fact]
    public void GreyColor_ColorName_Correct()
    {
        var tileColor = new GreyColorBehaviorStrategy();
        Assert.Equal(ColorName.Grey, tileColor.ColorName);
    }
    
    [Fact]
    public void WhiteColor_ColorName_Correct()
    {
        var tileColor = new NullColorBehaviorStrategy();
        Assert.Equal(ColorName.White, tileColor.ColorName);
    }
    
    [Fact]
    public void RedColor_ColorName_Correct()
    {
        var tileColor = new RedColorBehaviorStrategy();
        Assert.Equal(ColorName.Red, tileColor.ColorName);
    }
    
    [Fact]
    public void YellowColor_ColorName_Correct()
    {
        var tileColor = new YellowColorBehaviorStrategy();
        Assert.Equal(ColorName.Yellow, tileColor.ColorName);
    }
}