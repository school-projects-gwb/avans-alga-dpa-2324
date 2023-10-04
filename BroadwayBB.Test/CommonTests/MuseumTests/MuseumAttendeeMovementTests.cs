using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Museum;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Test.CommonTests.MuseumTests;

public class MuseumAttendeeMovementTests : TileTestBase
{
    [Fact]
    public void Museum_MoveAttendees_AttendeeMoves()
    {
        var museum = new Museum();
        var attendee = new Artist(new Coords(0, 0), 0, 2);
        double previousAttendeePosX = attendee.Movement.GridPos.Xd;
        
        var tiles = CreateWhiteColorTestGrid();
        var attendees = new List<IAttendee> { attendee };

        museum.SetData(tiles, attendees);
        museum.Config.Toggle(ConfigType.ShouldMoveAttendees);
        museum.MoveAttendees();
        
        Assert.NotEqual(previousAttendeePosX, attendee.Movement.GridPos.Xd);
    }
    
    [Fact]
    public void Museum_MoveAttendees_AttendeeCollides_TileChangesColor()
    {
        var collisionTilePos = new Coords(1, 0);
        
        var museum = new Museum();
        var attendee = new Artist(new Coords(0, 0), 0, 15);
        
        var tiles = CreateWhiteColorGridWithGivenColor(collisionTilePos, new RedColorBehaviorStrategy());
        var attendees = new List<IAttendee> { attendee };
        
        museum.SetData(tiles, attendees);
        museum.Config.Toggle(ConfigType.ShouldMoveAttendees);
        museum.MoveAttendees();
        
        Assert.IsType<BlueColorBehaviorStrategy>(museum.Tiles.Find(tile => Coords.IntEqual(tile.Pos, collisionTilePos))?.ColorBehaviorStrategy);
    }
    
    [Fact]
    public void Museum_MoveAttendees_AttendeeCollides_ArtistGetsRemoved()
    {
        var collisionTilePos = new Coords(1, 0);
        
        var museum = new Museum();
        var attendee = new Artist(new Coords(0, 0), 0, 15);
        
        var tiles = CreateWhiteColorGridWithGivenColor(collisionTilePos, new RedColorBehaviorStrategy());
        var attendees = new List<IAttendee> { attendee };
        
        museum.SetData(tiles, attendees);
        museum.Config.Toggle(ConfigType.ShouldMoveAttendees);
        museum.MoveAttendees();
        
        Assert.Empty(museum.Attendees);
    }
}