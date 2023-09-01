using BroadwayBB.Common.Behaviors.Interfaces;

namespace BroadwayBB.Common.Models.Interfaces;

public interface ICollisionResult
{
    public Dictionary<ITile, IColorBehavior> UpdatedTileColorBehavior { get; }
    public bool ShouldCreateArtist { get; }
    public bool ShouldRemoveArtist { get; }
}