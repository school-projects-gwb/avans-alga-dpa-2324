using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Test.CommonTests.MuseumTests;

public class MuseumAttendeeMovementTests : TileTestBase
{
    [Fact]
    public void Museum_MoveAttendees_AttendeeMoves()
    {
        var museum = new Museum();
        var attendee = new Artist(0, 0, 0, 2);
        double previousAttendeePosX = attendee.Movement.GridPosX;
        
        museum.Tiles = CreateWhiteColorTestGrid();
        museum.Attendees = new List<IAttendee> { attendee };
        
        museum.MoveAttendees();
        
        Assert.NotEqual(previousAttendeePosX, attendee.Movement.GridPosX);
    }
    
    [Fact]
    public void Museum_MoveAttendees_AttendeeCollides_TileChangesColor()
    {
        int collisionTilePosX = 1, collisionTilePosY = 0;
        
        var museum = new Museum();
        var attendee = new Artist(0, 0, 0, 15);
        
        museum.Tiles = CreateWhiteColorGridWithGivenColor(collisionTilePosX,collisionTilePosY, new RedTileColor());
        museum.Attendees = new List<IAttendee> { attendee };
        
        museum.MoveAttendees();
        
        Assert.IsType<BlueTileColor>(museum.Tiles.Find(tile =>
            tile.PosX == collisionTilePosX && tile.PosY == collisionTilePosY)?.TileColorBehavior);
    }
    
    [Fact]
    public void Museum_MoveAttendees_AttendeeCollides_ArtistGetsRemoved()
    {
        int collisionTilePosX = 1, collisionTilePosY = 0;
        
        var museum = new Museum();
        var attendee = new Artist(0, 0, 0, 15);
        
        museum.Tiles = CreateWhiteColorGridWithGivenColor(collisionTilePosX,collisionTilePosY, new RedTileColor());
        museum.Attendees = new List<IAttendee> { attendee };
        
        museum.MoveAttendees();
        
        Assert.Empty(museum.Attendees);
    }
}