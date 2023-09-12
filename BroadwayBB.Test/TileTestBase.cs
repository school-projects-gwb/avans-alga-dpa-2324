using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Test;

public abstract class TileTestBase
{
    private static int colRowAmount = 3;
    private Type defaultColorType = typeof(WhiteTileColor);
    private ITileColorBehavior defaultColorBehavior = new WhiteTileColor();

    protected Dictionary<string, (int, int)> gridEdges = new Dictionary<string, (int, int)>
    {
        { "topLeft", (0, 0) },
        { "topRight", (colRowAmount-1, 0) },
        { "bottomLeft", (0, colRowAmount-1) },
        { "bottomRight", (colRowAmount-1, colRowAmount-1) },
    };
    
    protected List<ITile> CreateWhiteColorTestGrid()
    {
        List<ITile> tiles = new();

        for (int y = 0; y < colRowAmount; y++)
            for (int x = 0; x < colRowAmount; x++)
                tiles.Add(new Tile(x, y, new WhiteTileColor()));

        return tiles;
    }
    
    protected List<ITile> CreateWhiteColorGridWithTopLeftColor(int targetPosX, int targetPosY,  ITileColorBehavior targetColorBehavior)
    {
        List<ITile> tiles = new();

        for (int y = 0; y < colRowAmount; y++)
            for (int x = 0; x < colRowAmount; x++)
                if (x == targetPosX && y == targetPosY)
                    tiles.Add(new Tile(x, y, targetColorBehavior));
                else
                    tiles.Add( new Tile(x, y, defaultColorBehavior.DeepCopy()));
        
        return tiles;
    }

    protected int GetAdjacentTileColorChangedAmount(List<ITile> tiles, int targetPosX, int targetPosY)
    {
        int changedCounter = 0;
        
        for (int y = targetPosY - 1; y <= targetPosY + 1; y++)
        {
            for (int x = targetPosX - 1; x <= targetPosX + 1; x++)
            {
                int x1 = x, y1 = y;
                var tile = tiles.Find(tile => tile.PosX == x1 && tile.PosY == y1);

                if (tile == null || (x1 == targetPosX && y1 == targetPosY)) continue;
                if (tile.TileColorBehavior.GetType() != defaultColorType) changedCounter++;
            }
        }

        return changedCounter;
    }
}