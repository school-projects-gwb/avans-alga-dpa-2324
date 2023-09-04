using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities;

public class Museum
{
    private readonly TileManager _tileManager = new();
    
    public List<ITile> Tiles { 
        get => _tileManager.Tiles;
        set => _tileManager.Tiles = value;
    }
    
    public List<IAttendee> Artists { get; private set; } = new();

    public void SetArtists(List<IAttendee> artists) => Artists = artists;

    public void MoveAttendees()
    {
        foreach (var artist in Artists)
        {
            int currentPosX = (int) Math.Floor(artist.Movement.GridPosX);
            int currentPosY = (int) Math.Floor(artist.Movement.GridPosY);
            var possibleDirections = _tileManager.GetRelativeTilePositions(currentPosX, currentPosY);
            var movementResult = artist.Movement.HandleMovement(possibleDirections);
            if (movementResult.HasEnteredNewGridTile) HandleAttendeeMovementCollision(movementResult);
        }
    }
    
    private void HandleAttendeeMovementCollision(MovementResult movementResult)
    {
        var tileCollisionResult = _tileManager.HandleCollision(movementResult.GridPosX, movementResult.GridPosY);
    }
}