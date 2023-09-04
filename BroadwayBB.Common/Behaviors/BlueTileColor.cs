using System.Drawing;
using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Behaviors;

public class BlueTileColor : ITileColorBehavior
{
    public ColorName ColorName => ColorName.Blue;

    public ColorBehaviorResult HandleCollision()
    {
        return new ColorBehaviorResult
        {
            UpdatedCollisionTargetTileColor = new YellowTileColor(), 
            ShouldCreateArtist = true, 
            UpdatedAdjacentTileColors = new List<ITileColorBehavior>{ new BlueTileColor(), new BlueTileColor() }
        };
    }
}