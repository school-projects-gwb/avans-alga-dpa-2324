using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models;

public class CollisionResult : ICollisionResult
{
    public Dictionary<ITile, IColorBehavior> UpdatedTileColorBehavior { get; }
    public bool ShouldCreateArtist { get; }
    public bool ShouldRemoveArtist { get; }

    public CollisionResult(Dictionary<ITile, IColorBehavior> updatedTileColorBehavior, bool shouldCreateArtist,
        bool shouldRemoveArtist)
    {
        UpdatedTileColorBehavior = updatedTileColorBehavior;
        ShouldCreateArtist = shouldCreateArtist;
        ShouldRemoveArtist = shouldRemoveArtist;
    } 
}