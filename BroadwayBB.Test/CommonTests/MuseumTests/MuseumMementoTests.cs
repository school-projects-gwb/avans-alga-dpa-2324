using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Test.CommonTests.MuseumTests;

public class MuseumMementoTests
{
    [Fact]
    public void Museum_CreateMemento_Correct()
    {
        var museum = new Museum();
        museum.Attendees = new List<IAttendee> { new Artist(0, 0, 0, 0) };
        museum.Tiles = new List<ITile> { new Tile(0, 0, new WhiteTileColor()) };
    }
    
    [Fact]
    public void Museum_RewindWithNoPreviousMemento_NoChanges()
    {
        var museum = new Museum();
        museum.Attendees = new List<IAttendee> { new Artist(0, 0, 0, 0) };
        museum.Tiles = new List<ITile> { new Tile(0, 0, new WhiteTileColor()) };
        
        var previousAttendees = museum.Attendees;
        var previousTiles = museum.Tiles;
        
        museum.RewindMemento();
        Assert.Same(previousAttendees, museum.Attendees);
        Assert.Same(previousTiles, museum.Tiles);
    }
    
    [Fact]
    public void Museum_RewindWithValidPreviousMemento_RewindCorrect()
    {
        var museum = new Museum();
        museum.Attendees = new List<IAttendee> { new Artist(0, 0, 0, 0) };
        museum.Tiles = new List<ITile> { new Tile(0, 0, new WhiteTileColor()) };
        
        var previousAttendees = museum.Attendees;
        var previousTiles = museum.Tiles;
        
        museum.CreateMemento();
        museum.RewindMemento();
        
        Assert.NotSame(previousAttendees, museum.Attendees);
        Assert.NotSame(previousTiles, museum.Tiles);
    }
}