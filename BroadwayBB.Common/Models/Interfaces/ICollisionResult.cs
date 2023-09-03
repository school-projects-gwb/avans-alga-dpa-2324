using BroadwayBB.Common.Behaviors.Interfaces;

namespace BroadwayBB.Common.Models.Interfaces;

public interface ICollisionResult
{
    public ITileColorBehavior UpdatedCollisionTargetTileTileColor { get; }
    public List<ITileColorBehavior>? UpdatedAdjacentTileColors { get; }
    public bool ShouldCreateArtist { get; }
    public bool ShouldRemoveArtist { get; }
}