using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Common.Models;

public class Museum
{
    public List<ITile> Tiles { get; private set; }
    public List<IAttendee> Artists { get; private set; }
    
    public void SetTiles(List<ITile> tiles) => Tiles = tiles;

    public void SetArtists(List<IAttendee> artists) => Artists = artists;

    public void MoveAttendees()
    {
        foreach (var artist in Artists)
            artist.Movement.Move(GetPossibleAttendeeDirections(artist));
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
}