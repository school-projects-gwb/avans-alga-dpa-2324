using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Test.CommonTests.AttendeeTests;

public class AttendeeMovementTests : TileTestBase
{
    [Fact]
    public void AttendeeMovement_NoPossibleDirections_IsDefaultResult()
    {
        var artist = new Artist(new Coords(2,2), 2, 0);
        List<MovementDirection> movementDirections = new();

        var result = artist.Movement.HandleMovement(movementDirections);

        Assert.Equal(0, result.GridPos.Xd);
        Assert.Equal(0, result.GridPos.Yd);
        Assert.False(result.HasEnteredNewGridTile);
    }
    
    [Fact]
    public void AttendeeMovement_HorizontalSpeedPositionInCorner_ResultMoveEast()
    {
        var artist = new Artist(new Coords(0,0), 0, 2);
        double previousGridPosX = artist.Movement.GridPos.Xd;
        double previousGridPosY = artist.Movement.GridPos.Yd;
        
        List<MovementDirection> movementDirections = new() { MovementDirection.East, MovementDirection.South};
        
        artist.Movement.HandleMovement(movementDirections);

        Assert.NotEqual(previousGridPosX, artist.Movement.GridPos.Xd);
        Assert.Equal(previousGridPosY, artist.Movement.GridPos.Yd);
    }
    
    [Fact]
    public void AttendeeMovement_VerticalSpeedPositionInCorner_ResultMoveSouth()
    {
        var artist = new Artist(new Coords(0,0), 2, 0);
        double previousGridPosX = artist.Movement.GridPos.Xd;
        double previousGridPosY = artist.Movement.GridPos.Yd;
        
        List<MovementDirection> movementDirections = new() { MovementDirection.East, MovementDirection.South};
        
        artist.Movement.HandleMovement(movementDirections);

        Assert.Equal(previousGridPosX, artist.Movement.GridPos.Xd);
        Assert.NotEqual(previousGridPosY, artist.Movement.GridPos.Yd);
    }
    
    [Fact]
    public void AttendeeMovement_FastHorizontalSpeed_ResultMoveEastNewTile()
    {
        var artist = new Artist(new Coords(0,0), 0, 20);
        double previousGridPosX = artist.Movement.GridPos.Xd;
        double previousGridPosY = artist.Movement.GridPos.Yd;
        
        List<MovementDirection> movementDirections = new() { MovementDirection.East, MovementDirection.South};
        
        var result = artist.Movement.HandleMovement(movementDirections);

        Assert.NotEqual(previousGridPosX, artist.Movement.GridPos.Xd);
        Assert.Equal(previousGridPosY, artist.Movement.GridPos.Yd);
        Assert.True(result.HasEnteredNewGridTile);
    }
}