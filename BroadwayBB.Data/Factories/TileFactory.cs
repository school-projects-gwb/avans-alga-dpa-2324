using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;
using BroadwayBB.Data.Factories.Interfaces;

namespace BroadwayBB.Data.Factories;

public class TileFactory : ITileFactory
{
    public ITile Create(Coords coords, char tag)
    {
        return tag switch
        {
            'B' => new Tile(coords, new BlueColorBehaviorStrategy()),
            'Y' => new Tile(coords, new YellowColorBehaviorStrategy()),
            'R' => new Tile(coords, new RedColorBehaviorStrategy()),
            'G' => new Tile(coords, new GreyColorBehaviorStrategy()),
            _ => new Tile(coords, new NullColorBehaviorStrategy())
        };
    }
}