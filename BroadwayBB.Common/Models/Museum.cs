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
        {
            artist.Movement.Move(GetPossibleAttendeeDirections(artist));
        }
    }

    private List<MovementDirection> GetPossibleAttendeeDirections(IAttendee attendee)
    {
        int maxX = Tiles.Max(tile => tile.PosX);
        int maxY = Tiles.Max(tile => tile.PosY);
        var possibleDirections = new List<MovementDirection>();

        if (attendee.Movement.GridPosY > 0 && Tiles.Any(tile => tile.PosX == attendee.Movement.GridPosX && tile.PosY == attendee.Movement.GridPosY - 1 && tile.CanMove(attendee)))
            possibleDirections.Add(MovementDirection.North);

        if (attendee.Movement.GridPosY < maxY && Tiles.Any(tile => tile.PosX == attendee.Movement.GridPosX && tile.PosY == attendee.Movement.GridPosY + 1 && tile.CanMove(attendee)))
            possibleDirections.Add(MovementDirection.South);

        if (attendee.Movement.GridPosX > 0 && Tiles.Any(tile => tile.PosX == attendee.Movement.GridPosX - 1 && tile.PosY == attendee.Movement.GridPosY && tile.CanMove(attendee)))
            possibleDirections.Add(MovementDirection.West);

        if (attendee.Movement.GridPosX < maxX && Tiles.Any(tile => tile.PosX == attendee.Movement.GridPosX + 1 && tile.PosY == attendee.Movement.GridPosY && tile.CanMove(attendee)))
            possibleDirections.Add(MovementDirection.East);

        return possibleDirections;
    }
}