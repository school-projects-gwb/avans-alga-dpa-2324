using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities;

public class TileManager
{
    public List<ITile> Tiles { get; set; }
    
    public List<MovementDirection> GetRelativeTilePositions(int currentTilePosX, int currentTilePosY)
    {
        var possibleDirections = new List<MovementDirection>();
        
        if (Tiles.Find(tile => tile.PosX == currentTilePosX && tile.PosY == currentTilePosY - 1) != null)
            possibleDirections.Add(MovementDirection.North);
        
        if (Tiles.Find(tile => tile.PosX == currentTilePosX + 1 && tile.PosY == currentTilePosY) != null) 
            possibleDirections.Add(MovementDirection.East);
        
        if (Tiles.Find(tile => tile.PosX == currentTilePosX && tile.PosY == currentTilePosY + 1) != null) 
            possibleDirections.Add(MovementDirection.South);
        
        if (Tiles.Find(tile => tile.PosX == currentTilePosX - 1 && tile.PosY == currentTilePosY) != null) 
            possibleDirections.Add(MovementDirection.West);
        
        return possibleDirections;
    }

    public TileCollisionResult HandleCollision(int tilePosX, int tilePosY)
    {
        var collisionResult = new TileCollisionResult();
        var targetTile = FindTile(tilePosX, tilePosY);
        if (targetTile == null) return collisionResult;
        
        var colorBehaviorResult = targetTile.TileColorBehavior.HandleCollision();
        targetTile.UpdateColorBehavior(colorBehaviorResult.UpdatedCollisionTargetTileColor);
        UpdateAdjacentTiles(targetTile, colorBehaviorResult.UpdatedAdjacentTileColors);

        collisionResult.ShouldCreateArtist = colorBehaviorResult!.ShouldCreateArtist;
        collisionResult.ShouldRemoveArtist = colorBehaviorResult!.ShouldRemoveArtist;
        
        return collisionResult;
    }

    private void UpdateAdjacentTiles(ITile relativeTile, List<ITileColorBehavior> updatedAdjacentTileColors)
    {
        if (updatedAdjacentTileColors.Count == 0) return;
        var relativeGridPositions = new List<(int posX, int posY)> { (-1, 0), (1, 0), (0, -1), (0, 1) };
        var random = new Random();

        while (updatedAdjacentTileColors.Count > 0 && relativeGridPositions.Count > 0)
        {
            var randomDirection = relativeGridPositions[random.Next(relativeGridPositions.Count)];
            int adjacentX = relativeTile.PosX + randomDirection.posX, 
                adjacentY = relativeTile.PosY + randomDirection.posY;
            var adjacentTile = Tiles.FirstOrDefault(tile => tile.PosX == adjacentX && tile.PosY == adjacentY);
            
            if (adjacentTile != null)
            {
                adjacentTile.UpdateColorBehavior(updatedAdjacentTileColors.First());
                updatedAdjacentTileColors.Remove(updatedAdjacentTileColors.First());
            }
            else
            {
                relativeGridPositions.Remove(randomDirection);
            }
        }
    }
    
    private ITile? FindTile(int posX, int posY) => Tiles.Find(tile => tile.PosX == posX && tile.PosY == posY);
    
    public List<ITile> CreateMemento() => Tiles.Select(tile => tile.DeepCopy()).ToList();
}