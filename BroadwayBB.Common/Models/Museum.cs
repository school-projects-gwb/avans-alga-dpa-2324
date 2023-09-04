using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models;

public class Museum
{
    public List<ITile> Tiles { get; private set; } = new();
    public List<IAttendee> Artists { get; private set; } = new();
    
    public void SetTiles(List<ITile> tiles) => Tiles = tiles;

    public void SetArtists(List<IAttendee> artists) => Artists = artists;

    public void MoveAttendees()
    {
        foreach (var artist in Artists)
        {
            var movementResult = artist.Movement.HandleMovement(GetPossibleAttendeeDirections(artist));
            HandleAttendeeMovementCollision(movementResult);
        }
    }

    private List<MovementDirection> GetPossibleAttendeeDirections(IAttendee attendee)
    {
        var possibleDirections = new List<MovementDirection>();

        int currentPosX = (int) Math.Round(attendee.Movement.GridPosX);
        int currentPosY = (int) Math.Round(attendee.Movement.GridPosY);
        
        if (Tiles.Find(tile => tile.PosX == currentPosX && tile.PosY == currentPosY - 1 && tile.CanMove(attendee)) != null)
            possibleDirections.Add(MovementDirection.North);
        
        if (Tiles.Find(tile => tile.PosX == currentPosX + 1 && tile.PosY == currentPosY && tile.CanMove(attendee)) != null) 
            possibleDirections.Add(MovementDirection.East);
        
        if (Tiles.Find(tile => tile.PosX == currentPosX && tile.PosY == currentPosY + 1 && tile.CanMove(attendee)) != null) 
            possibleDirections.Add(MovementDirection.South);
        
        if (Tiles.Find(tile => tile.PosX == currentPosX - 1 && tile.PosY == currentPosY && tile.CanMove(attendee)) != null) 
            possibleDirections.Add(MovementDirection.West);
        
        return possibleDirections;
    }
    
    private void HandleAttendeeMovementCollision(MovementResult movementResult)
    {
        if (!movementResult.HasEnteredNewGridTile) return;
        var targetTile = Tiles.Find(tile => tile.PosX == movementResult.GridPosX && tile.PosY == movementResult.GridPosY);
        targetTile?.TileColorBehavior.HandleCollision();
    }
}