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
    
    public List<MovementDirection> GetAllowedRelativeTilePositions(Coords currentTilePos)
    {
        var possibleDirections = new List<MovementDirection>();

        var currentNode = FindNode(currentTilePos);
        
        if (currentNode == null) return possibleDirections;

        foreach (var neighborNode in currentNode.Neighbors)
        {
            if (neighborNode.Tile.Pos.Xi == currentNode.Tile.Pos.Xi && neighborNode.Tile.Pos.Yi == currentNode.Tile.Pos.Yi - 1)
                possibleDirections.Add(MovementDirection.North);

            if (neighborNode.Tile.Pos.Xi == currentNode.Tile.Pos.Xi + 1 && neighborNode.Tile.Pos.Yi == currentNode.Tile.Pos.Yi)
                possibleDirections.Add(MovementDirection.East);

            if (neighborNode.Tile.Pos.Xi == currentNode.Tile.Pos.Xi && neighborNode.Tile.Pos.Yi == currentNode.Tile.Pos.Yi + 1)
                possibleDirections.Add(MovementDirection.South);

            if (neighborNode.Tile.Pos.Xi == currentNode.Tile.Pos.Xi - 1 && neighborNode.Tile.Pos.Yi == currentNode.Tile.Pos.Yi)
                possibleDirections.Add(MovementDirection.West);
        }

        return possibleDirections;
    }

    public TileCollisionResult HandleCollision(Coords tilePos)
    {
        // TileCollisionResult uitbreiden met IsTileInPath
        // Logica uitbreiden met check of attendee op path tile staat
        var collisionResult = new TileCollisionResult();
        var targetNode = FindNode(tilePos);
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

    private TileNode? FindNode(Coords tilePos) => _tileGraph.FirstOrDefault(node => Coords.IntEqual(node.Tile.Pos, tilePos));


    public List<ITile> CreateMemento() => Tiles.Select(tile => tile.DeepCopy()).ToList();
    
    void ConnectOrthogonalNeighbors(TileNode currentNode, List<TileNode> allNodes)
    {
        var neighborOffsets = new List<Coords> { new (-1, 0), new (1, 0), new (0, -1), new (0, 1) };

        foreach (var offset in neighborOffsets)
        {
            var neighbor = currentNode.Tile.Pos + offset;
            
            var neighborNode = allNodes.FirstOrDefault(node => Coords.IntEqual(node.Tile.Pos, neighbor));

            if (neighborNode == null) continue;
            
            currentNode.Neighbors.Add(neighborNode);
            neighborNode.Neighbors.Add(currentNode);
        }
    }
}