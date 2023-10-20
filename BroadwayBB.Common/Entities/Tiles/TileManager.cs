using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Attendees.PathFinder;
using BroadwayBB.Common.Entities.Museum.Mediator;
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

    public TilePathfinder TilePathfinder { get; } = new();

    private List<TileNode> _tileGraph = new();

    private IMuseumMediator _museumMediator;

    public TileManager(IMuseumMediator museumMediator) => _museumMediator = museumMediator;

    public void HandleMovement(List<IAttendee> attendees)
    {
        foreach (var attendee in attendees)
        {
            var possibleDirections = GetAllowedRelativeTilePositions(
                attendee.Movement.GridPos
            );
            
            attendee.Movement.IsColliding = false;
            _museumMediator.Notify(new AttendeeMovementNotification(attendee, possibleDirections));
        }
        
        _museumMediator.Notify(new MovementFinishedNotification());
    }
    
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
        TilePathfinder?.SetTiles(_tileGraph);
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

    public void CheckCollision(Coords tilePos, bool isTileBehavior)
    {
        var collisionResult = HandleCollision(tilePos, isTileBehavior);
        _museumMediator.Notify(new TileCollisionNotification(collisionResult));
    }
    
    public TileCollisionResult HandleCollision(Coords tilePos, bool isTileBehavior)
    {
        var collisionResult = new TileCollisionResult();
        var targetNode = FindNode(tilePos);
        if (targetNode == null) return collisionResult;

        var colorBehaviorResult = targetNode.Tile.ColorBehaviorStrategy.HandleCollision();
        if (isTileBehavior)
        {
            targetNode.Tile.UpdateColorBehavior(colorBehaviorResult.UpdatedCollisionTargetColor);
            UpdateAdjacentTiles(targetNode, colorBehaviorResult.UpdatedAdjacentTileColors);
            collisionResult.ShouldCreateArtist = colorBehaviorResult.ShouldCreateArtist;
            collisionResult.ShouldRemoveArtist = colorBehaviorResult.ShouldRemoveArtist;
        }

        collisionResult.IsInPath = TilePathfinder.IsTileInPath(targetNode);

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
    
    public List<ITile> GetTileClones() => Tiles.Select(tile => tile.Clone()).ToList();
    
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

    public void GeneratePath(Coords startTilePosition, Coords targetTilePosition)
    {
        var target = FindNode(startTilePosition);
        var start = FindNode(targetTilePosition);

        if (start == null || target == null) return;
        TilePathfinder.GeneratePath(start.Tile, target.Tile);
    }

    public IEnumerable<DebugTile> GetPathfinderDebugInfo(bool withVisited) => TilePathfinder.GetDebugInfo(withVisited);
}