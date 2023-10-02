using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;

namespace BroadwayBB.Test.CommonTests.AttendeeTests;

public class AttendeeMovementTests : TileTestBase
{
    [Fact]
    public void AttendeeMovement_NoPossibleDirections_IsDefaultResult()
    {
        var artist = new Artist(2,2, 2, 0);
        List<MovementDirection> movementDirections = new();

        var result = artist.Movement.HandleMovement(movementDirections);

        Assert.Equal(0, result.GridPosX);
        Assert.Equal(0, result.GridPosY);
        Assert.False(result.HasEnteredNewGridTile);
    }
    
    [Fact]
    public void AttendeeMovement_HorizontalSpeedPositionInCorner_ResultMoveEast()
    {
        var artist = new Artist(0,0, 0, 2);
        double previousGridPosX = artist.Movement.GridPosX;
        double previousGridPosY = artist.Movement.GridPosY;
        
        List<MovementDirection> movementDirections = new() { MovementDirection.East, MovementDirection.South};
        
        artist.Movement.HandleMovement(movementDirections);

        Assert.NotEqual(previousGridPosX, artist.Movement.GridPosX);
        Assert.Equal(previousGridPosY, artist.Movement.GridPosY);
    }
    
    [Fact]
    public void AttendeeMovement_VerticalSpeedPositionInCorner_ResultMoveSouth()
    {
        var artist = new Artist(0,0, 2, 0);
        double previousGridPosX = artist.Movement.GridPosX;
        double previousGridPosY = artist.Movement.GridPosY;
        
        List<MovementDirection> movementDirections = new() { MovementDirection.East, MovementDirection.South};
        
        artist.Movement.HandleMovement(movementDirections);

        Assert.Equal(previousGridPosX, artist.Movement.GridPosX);
        Assert.NotEqual(previousGridPosY, artist.Movement.GridPosY);
    }
    
    [Fact]
    public void AttendeeMovement_FastHorizontalSpeed_ResultMoveEastNewTile()
    {
        var artist = new Artist(0,0, 0, 20);
        double previousGridPosX = artist.Movement.GridPosX;
        double previousGridPosY = artist.Movement.GridPosY;
        
        List<MovementDirection> movementDirections = new() { MovementDirection.East, MovementDirection.South};
        
        var result = artist.Movement.HandleMovement(movementDirections);

        Assert.NotEqual(previousGridPosX, artist.Movement.GridPosX);
        Assert.Equal(previousGridPosY, artist.Movement.GridPosY);
        Assert.True(result.HasEnteredNewGridTile);
    }
}