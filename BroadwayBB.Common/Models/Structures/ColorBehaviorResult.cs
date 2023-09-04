using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models.Structures;

public class ColorBehaviorResult
{
    public ITileColorBehavior UpdatedCollisionTargetTileColor;
    public List<ITileColorBehavior> UpdatedAdjacentTileColors = new();
    public bool ShouldCreateArtist = false;
    public bool ShouldRemoveArtist = false;
}