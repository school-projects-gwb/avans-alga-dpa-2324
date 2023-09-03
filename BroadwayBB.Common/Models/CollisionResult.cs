using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models;

public class CollisionResult : ICollisionResult
{
    public ITileColorBehavior UpdatedCollisionTargetTileTileColor { get; }
    public List<ITileColorBehavior>? UpdatedAdjacentTileColors { get; }
    public bool ShouldCreateArtist { get; }
    public bool ShouldRemoveArtist { get; }

    public CollisionResult(
        ITileColorBehavior updatedCollisionTargetTileTileColor, 
        List<ITileColorBehavior>? updatedAdjacentTileColors = null,
        bool shouldCreateArtist = false,
        bool shouldRemoveArtist = false)
    {
        UpdatedCollisionTargetTileTileColor = updatedCollisionTargetTileTileColor;
        UpdatedAdjacentTileColors = updatedAdjacentTileColors;
        ShouldCreateArtist = shouldCreateArtist;
        ShouldRemoveArtist = shouldRemoveArtist;
    } 
}