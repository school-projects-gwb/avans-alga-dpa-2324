using BroadwayBB.Common.Behaviors.Interfaces;

namespace BroadwayBB.Common.Behaviors;

public class ColorBehaviorResult
{
    public IColorBehaviorStrategy UpdatedCollisionTargetColor;
    public List<IColorBehaviorStrategy> UpdatedAdjacentTileColors = new();
    public bool ShouldCreateArtist = false;
    public bool ShouldRemoveArtist = false;
}