using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Test.CommonTests;

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
    
    protected List<ITile> CreateWhiteColorGridWithGivenColor(int targetPosX, int targetPosY,  ITileColorBehavior targetColorBehavior)
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
        
        var relativeGridPositions = new List<(int posX, int posY)> { (-1, 0), (1, 0), (0, -1), (0, 1) };
        var random = new Random();

        while (relativeGridPositions.Count > 0)
        {
            var randomDirection = relativeGridPositions[random.Next(relativeGridPositions.Count)];
            int adjacentX = targetPosX + randomDirection.posX, 
                adjacentY = targetPosY + randomDirection.posY;
            var adjacentTile = tiles.FirstOrDefault(tile => tile.PosX == adjacentX && tile.PosY == adjacentY);

            if (adjacentTile != null) if (adjacentTile.TileColorBehavior.GetType() != defaultColorType) changedCounter++;
            
            relativeGridPositions.Remove(randomDirection);
        }

        return changedCounter;
    }
}