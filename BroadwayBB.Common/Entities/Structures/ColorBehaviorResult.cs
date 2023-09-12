using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Common.Entities.Structures;

public class ColorBehaviorResult
{
    public ITileColorBehavior UpdatedCollisionTargetTileColor;
    public List<ITileColorBehavior> UpdatedAdjacentTileColors = new();
    public bool ShouldCreateArtist = false;
    public bool ShouldRemoveArtist = false;
}