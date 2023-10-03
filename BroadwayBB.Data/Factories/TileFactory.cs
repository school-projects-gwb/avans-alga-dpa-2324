using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Data.Factories.Interfaces;

namespace BroadwayBB.Data.Factories;

public class TileFactory : ITileFactory
{
    public ITile Create(int posX, int posY, char color)
    {
        return color switch
        {
            'B' => new Tile(posX, posY, new BlueColorBehaviorStrategy()),
            'Y' => new Tile(posX, posY, new YellowColorBehaviorStrategy()),
            'R' => new Tile(posX, posY, new RedColorBehaviorStrategy()),
            'G' => new Tile(posX, posY, new GreyColorBehaviorStrategy()),
            _ => new Tile(posX, posY, new WhiteColorBehaviorStrategy())
        };
    }
}