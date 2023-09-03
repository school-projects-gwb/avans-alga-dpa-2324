using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models;

public class CollisionResult : ICollisionResult
{
    public IColorBehavior UpdatedCollisionTargetTileColor { get; }
    public List<IColorBehavior>? UpdatedAdjacentTileColors { get; }
    public bool ShouldCreateArtist { get; }
    public bool ShouldRemoveArtist { get; }

    public CollisionResult(
        IColorBehavior updatedCollisionTargetTileColor, 
        List<IColorBehavior>? updatedAdjacentTileColors = null,
        bool shouldCreateArtist = false,
        bool shouldRemoveArtist = false)
    {
        UpdatedCollisionTargetTileColor = updatedCollisionTargetTileColor;
        UpdatedAdjacentTileColors = updatedAdjacentTileColors;
        ShouldCreateArtist = shouldCreateArtist;
        ShouldRemoveArtist = shouldRemoveArtist;
    } 
}