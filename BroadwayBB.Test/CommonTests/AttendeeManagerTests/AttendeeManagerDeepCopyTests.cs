using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Test.CommonTests.AttendeeManagerTests;

public class AttendeeManagerDeepCopyTests
{
    [Fact]
    void TileManager_CreateMemento_Correct()
    {
        var attendeeManager = new AttendeeManager();
        var randomAttendee = new Artist(1, 1, 0, 0);
        attendeeManager.Attendees = new List<IAttendee> { randomAttendee };

        var attendeeCopy = attendeeManager.CreateMemento();

        Assert.NotSame(attendeeManager.Attendees, attendeeCopy);
    }
}