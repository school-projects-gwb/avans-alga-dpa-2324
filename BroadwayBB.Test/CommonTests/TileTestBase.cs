using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Test.CommonTests;

public abstract class TileTestBase
{
    private static int colRowAmount = 3;
    private Type defaultColorType = typeof(NullColorBehaviorStrategy);
    private IColorBehaviorStrategy _defaultColorBehaviorStrategy = new NullColorBehaviorStrategy();

    protected Dictionary<string, Coords> gridEdges = new()
    {
        { "topLeft", new Coords(0, 0) },
        { "topRight", new Coords(colRowAmount-1, 0) },
        { "bottomLeft", new Coords(0, colRowAmount - 1) },
        { "bottomRight", new Coords(colRowAmount - 1, colRowAmount - 1) },
    };
    
    protected List<ITile> CreateWhiteColorTestGrid()
    {
        List<ITile> tiles = new();

        for (int y = 0; y < colRowAmount; y++)
            for (int x = 0; x < colRowAmount; x++)
                tiles.Add(new Tile(new Coords(x, y), new NullColorBehaviorStrategy()));

        return tiles;
    }
    
    protected List<ITile> CreateWhiteColorGridWithGivenColor(Coords targetPos,  IColorBehaviorStrategy targetColorBehaviorStrategy)
    {
        List<ITile> tiles = new();

        for (int y = 0; y < colRowAmount; y++)
            for (int x = 0; x < colRowAmount; x++)
            {
                var current = new Coords(x, y);
                if (Coords.IntEqual(current, targetPos))
                    tiles.Add(new Tile(targetPos, targetColorBehaviorStrategy));
                else
                    tiles.Add(new Tile(current, _defaultColorBehaviorStrategy.Clone()));
            }
        
        return tiles;
    }

    protected int GetAdjacentTileColorChangedAmount(List<ITile> tiles, Coords targetPos)
    {
        int changedCounter = 0;
        
        var relativeGridPositions = new List<Coords> { new (-1, 0), new (1, 0), new (0, -1), new (0, 1) };
        var random = new Random();

        while (relativeGridPositions.Count > 0)
        {
            var randomDirection = relativeGridPositions[random.Next(relativeGridPositions.Count)];
            var adjecent = targetPos + randomDirection;
            var adjacentTile = tiles.FirstOrDefault(tile => Coords.IntEqual(tile.Pos, adjecent));

            if (adjacentTile != null) if (adjacentTile.ColorBehaviorStrategy.GetType() != defaultColorType) changedCounter++;
            
            relativeGridPositions.Remove(randomDirection);
        }

        return changedCounter;
    }
}