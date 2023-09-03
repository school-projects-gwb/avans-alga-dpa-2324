using BroadwayBB.Common.Behaviors.Interfaces;

namespace BroadwayBB.Common.Models.Interfaces;

public interface ICollisionResult
{
    public IColorBehavior UpdatedCollisionTargetTileColor { get; }
    public List<IColorBehavior>? UpdatedAdjacentTileColors { get; }
    public bool ShouldCreateArtist { get; }
    public bool ShouldRemoveArtist { get; }
}