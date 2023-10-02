using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Tiles;

public class TileManager
{
    private List<ITile> _tiles = new();
    public List<ITile> Tiles
    {
        get => _tiles;
        set => ProcessTiles(value);
    }

    private List<TileNode> _tileGraph = new();

    private void ProcessTiles(List<ITile> tiles)
    {
        _tiles = tiles;
        BuildTileGraph(tiles);
    }

    private void BuildTileGraph(List<ITile> tiles)
    {
        List<TileNode> nodes = new List<TileNode>();
        
        foreach (var tile in tiles)
        {
            var node = new TileNode(tile);
            nodes.Add(node);
            ConnectOrthogonalNeighbors(node, nodes);
        }

        _tileGraph = nodes;
    }
    
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
        // TileCollisionResult uitbreiden met IsTileInPath
        // Logica uitbreiden met check of attendee op path tile staat
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
    
    private TileNode? FindNode(int posX, int posY) => _tileGraph.FirstOrDefault(node => node.Tile.PosX == posX && node.Tile.PosY == posY);
    
    public List<ITile> CreateMemento() => Tiles.Select(tile => tile.DeepCopy()).ToList();
    
    void ConnectOrthogonalNeighbors(TileNode currentNode, List<TileNode> allNodes)
    {
        var neighborOffsets = new List<(int posX, int posY)> { (-1, 0), (1, 0), (0, -1), (0, 1) };
        var posX = currentNode.Tile.PosX;
        var posY = currentNode.Tile.PosY;

        foreach (var offset in neighborOffsets)
        {
            var neighborX = posX + offset.posX;
            var neighborY = posY + offset.posY;
            
            var neighborNode = allNodes.FirstOrDefault(node =>
                node.Tile.PosX == neighborX && node.Tile.PosY == neighborY);

            if (neighborNode == null) continue;
            
            currentNode.Neighbors.Add(neighborNode);
            neighborNode.Neighbors.Add(currentNode);
        }
    }
}