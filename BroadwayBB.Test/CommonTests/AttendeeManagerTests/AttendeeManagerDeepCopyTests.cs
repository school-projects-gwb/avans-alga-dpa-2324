using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Test.CommonTests.AttendeeManagerTests;

public class AttendeeManagerDeepCopyTests
{
    [Fact]
    void TileManager_CreateMemento_Correct()
    {
        var attendeeManager = new AttendeeManager();
        var randomAttendee = new Artist(new Coords(1, 1), 0, 0);
        attendeeManager.Attendees = new List<IAttendee> { randomAttendee };

        var attendeeCopy = attendeeManager.GetAttendeeClones();

        Assert.NotSame(attendeeManager.Attendees, attendeeCopy);
    }
}