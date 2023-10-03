using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Test.CommonTests.AttendeeManagerTests;

public class AttendeeManagerCollisionTests
{
    [Fact]
    public void AttendeeManager_CreateAttendeeFromCollisionResult_Correct()
    {
        var attendeeManager = new AttendeeManager();
        var randomAttendee = new Artist(1, 1, 0, 0);
        var tileCollisionResult = new TileCollisionResult
        {
            ShouldCreateArtist = true,
            ShouldRemoveArtist = false
        };
        
        attendeeManager.HandleTileCollisionResult(tileCollisionResult, randomAttendee);
        Assert.Empty(attendeeManager.Attendees);
        
        attendeeManager.HandleAttendeeQueue();
        Assert.Single(attendeeManager.Attendees);
    }
    
    [Fact]
    public void AttendeeManager_RemoveAttendeeFromCollisionResult_Correct()
    {
        var attendeeManager = new AttendeeManager();
        var randomAttendee = new Artist(1, 1, 0, 0);
        attendeeManager.Attendees = new List<IAttendee> { randomAttendee };
        
        var tileCollisionResult = new TileCollisionResult
        {
            ShouldCreateArtist = false,
            ShouldRemoveArtist = true
        };
        
        attendeeManager.HandleTileCollisionResult(tileCollisionResult, randomAttendee);
        Assert.Single(attendeeManager.Attendees);
        
        attendeeManager.HandleAttendeeQueue();
        Assert.Empty(attendeeManager.Attendees);
    }
}