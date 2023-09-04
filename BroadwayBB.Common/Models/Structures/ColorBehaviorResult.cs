using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models.Structures;

public class ColorBehaviorResult : ICollisionResult
{
    public ITileColorBehavior UpdatedCollisionTargetTileColor { get; }
    public List<ITileColorBehavior>? UpdatedAdjacentTileColors { get; }
    public bool ShouldCreateArtist { get; }
    public bool ShouldRemoveArtist { get; }

    public ColorBehaviorResult(
        ITileColorBehavior updatedCollisionTargetTileColor, 
        List<ITileColorBehavior>? updatedAdjacentTileColors = null,
        bool shouldCreateArtist = false,
        bool shouldRemoveArtist = false)
    {
        UpdatedCollisionTargetTileColor = updatedCollisionTargetTileColor;
        UpdatedAdjacentTileColors = updatedAdjacentTileColors;
        ShouldCreateArtist = shouldCreateArtist;
        ShouldRemoveArtist = shouldRemoveArtist;
    } 
}