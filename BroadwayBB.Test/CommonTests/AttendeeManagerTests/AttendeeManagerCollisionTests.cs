using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Test.CommonTests.AttendeeManagerTests;

public class AttendeeManagerCollisionTests
{
    private List<IAttendee> CreateAttendees(int amount = 20)
    {
        var attendees = new List<IAttendee>();
        
        for (int i = 0; i < amount; i++)
            attendees.Add(new Artist(0, 0, 2, 0));

        return attendees;
    }
    
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