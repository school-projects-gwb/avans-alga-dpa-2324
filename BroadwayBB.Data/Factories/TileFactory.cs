using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Models;
using BroadwayBB.Common.Models.Interfaces;
using BroadwayBB.Data.Factories.Interfaces;

namespace BroadwayBB.Data.Factories;

public class TileFactory : ITileFactory
{
    public ITile Create(int posX, int posY, char color)
    {
        return color switch
        {
            'B' => new Tile(posX, posY, new BlueTileColor()),
            'Y' => new Tile(posX, posY, new YellowTileColor()),
            'R' => new Tile(posX, posY, new RedTileColor()),
            'G' => new Tile(posX, posY, new GreyTileColor()),
            '_' => new Tile(posX, posY, new WhiteTileColor()),
            _ => new Tile(posX, posY, new WhiteTileColor())
        };
    }
}