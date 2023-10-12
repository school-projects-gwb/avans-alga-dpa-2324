using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Data.Factories.Interfaces;

public interface ITileFactory
{
    public ITile Create(ColorName name, Coords coords);
}