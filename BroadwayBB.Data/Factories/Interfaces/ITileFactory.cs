using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Data.Factories.Interfaces;

public interface ITileFactory
{
    public ITile Create(int posX, int posY, char color);
}