using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Data.Factories.Interfaces;

public interface ITileFactory
{
    public ITile Create(Coords coords, char tag);
}