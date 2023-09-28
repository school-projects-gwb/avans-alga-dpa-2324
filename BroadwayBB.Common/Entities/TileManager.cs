using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities;

public class TileManager
{
    public List<TileNode> Nodes { get; set; } = new();
    
    public List<MovementDirection> GetAllowedRelativeTilePositions(int currentTilePosX, int currentTilePosY)
    {
        var possibleDirections = new List<MovementDirection>();

        var currentNode = FindNode(currentTilePosX, currentTilePosY);
        
        if (currentNode == null) return possibleDirections;

        foreach (var neighborNode in currentNode.Neighbors)
        {
            if (neighborNode.Tile.PosX == currentNode.Tile.PosX && neighborNode.Tile.PosY == currentNode.Tile.PosY - 1)
                possibleDirections.Add(MovementDirection.North);

            if (neighborNode.Tile.PosX == currentNode.Tile.PosX + 1 && neighborNode.Tile.PosY == currentNode.Tile.PosY)
                possibleDirections.Add(MovementDirection.East);

            if (neighborNode.Tile.PosX == currentNode.Tile.PosX && neighborNode.Tile.PosY == currentNode.Tile.PosY + 1)
                possibleDirections.Add(MovementDirection.South);

            if (neighborNode.Tile.PosX == currentNode.Tile.PosX - 1 && neighborNode.Tile.PosY == currentNode.Tile.PosY)
                possibleDirections.Add(MovementDirection.West);
        }

        return possibleDirections;
    }

    public TileCollisionResult HandleCollision(int tilePosX, int tilePosY)
    {
        var collisionResult = new TileCollisionResult();
        var targetNode = FindNode(tilePosX, tilePosY);
        if (targetNode == null) return collisionResult;

        var colorBehaviorResult = targetNode.Tile.ColorBehaviorStrategy.HandleCollision();
        targetNode.Tile.UpdateColorBehavior(colorBehaviorResult.UpdatedCollisionTargetColor);
        UpdateAdjacentTiles(targetNode, colorBehaviorResult.UpdatedAdjacentTileColors);

        collisionResult.ShouldCreateArtist = colorBehaviorResult.ShouldCreateArtist;
        collisionResult.ShouldRemoveArtist = colorBehaviorResult.ShouldRemoveArtist;

        return collisionResult;
    }

    private void UpdateAdjacentTiles(TileNode node, List<IColorBehaviorStrategy> updatedAdjacentTileColors)
    {
        if (updatedAdjacentTileColors.Count == 0) return;

        foreach (var neighborNode in node.Neighbors)
        {
            if (updatedAdjacentTileColors.Count == 0) break;

            neighborNode.Tile.UpdateColorBehavior(updatedAdjacentTileColors.First());
            updatedAdjacentTileColors.RemoveAt(0);
        }
    }
    
    private TileNode? FindNode(int posX, int posY) => Nodes.FirstOrDefault(node => node.Tile.PosX == posX && node.Tile.PosY == posY);
    
    public List<TileNode> CreateMemento() => CreateTileNodeMemento();

    private List<TileNode> CreateTileNodeMemento()
    {
        // Deep copy the TileNodes without neighbors
        
        var deepCopies = Nodes.Select(node => node.DeepCopy()).ToList();

        foreach (var x in deepCopies)
        {
            ConnectOrthogonalNeighbors(x, deepCopies);
        }
        
        return deepCopies;
    }

    List<(int posX, int posY)> neighborOffsets = new List<(int posX, int posY)>
    {
        (-1, 0), (1, 0), (0, -1), (0, 1)
    };
    
    void ConnectOrthogonalNeighbors(TileNode currentNode, List<TileNode> allNodes)
    {
        int posX = currentNode.Tile.PosX;
        int posY = currentNode.Tile.PosY;

        foreach (var offset in neighborOffsets)
        {
            int neighborX = posX + offset.posX;
            int neighborY = posY + offset.posY;
            
            var neighborNode = allNodes.FirstOrDefault(node =>
                node.Tile.PosX == neighborX && node.Tile.PosY == neighborY);

            if (neighborNode == null) continue;
            
            currentNode.Neighbors.Add(neighborNode);
            neighborNode.Neighbors.Add(currentNode);
        }
    }
}