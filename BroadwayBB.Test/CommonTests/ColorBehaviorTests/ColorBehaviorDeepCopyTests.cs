using BroadwayBB.Common.Behaviors;

namespace BroadwayBB.Test.CommonTests.ColorBehaviorTests;

public class ColorBehaviorDeepCopyTests
{
    [Fact]
    public void ColorBehavior_DeepCopy_Correct()
    {
        var originalColorBehavior = new RedTileColor();
        var deepCopyColorBehavior = originalColorBehavior.DeepCopy();
        bool referenceEquals = ReferenceEquals(originalColorBehavior, deepCopyColorBehavior);
        
        Assert.False(referenceEquals);
    }
    
    [Fact]
    public void TileColorCounter_DeepCopy_Correct()
    {
        int tileCounterLimit = 2;
        var originalTileCounter = new TileColorCounter(tileCounterLimit);
        originalTileCounter.Increase(); // Value = 1
        originalTileCounter.Increase(); // Value = 2
        var deepCopyTileCounter = originalTileCounter.DeepCopy();
        
        bool referenceEquals = ReferenceEquals(originalTileCounter, deepCopyTileCounter);
        
        Assert.False(referenceEquals);
        Assert.True(deepCopyTileCounter.LimitReached()); // Value = 2
    }
}