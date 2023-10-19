using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Entities.Museum;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;
using BroadwayBB.Simulation;

namespace BroadwayBB.Test.CommonTests.MuseumTests;

public class MuseumMementoTests
{
    [Fact]
    public void Museum_CreateMemento_Correct()
    {
        var museum = new Museum();
        museum.Attendees = new List<IAttendee> { new Artist(new Coords(0, 0), 0, 0) };
        museum.Tiles = new List<ITile> { new Tile(new Coords(0, 0), new NullColorBehaviorStrategy()) };
    }
    
    [Fact]
    public void Museum_RewindWithNoPreviousMemento_NoChanges()
    {
        var museum = new Museum();
        museum.Attendees = new List<IAttendee> { new Artist(new Coords(0, 0), 0, 0) };
        museum.Tiles = new List<ITile> { new Tile(new Coords(0, 0), new NullColorBehaviorStrategy()) };
        
        MuseumSimulationFacade simulation = new(museum);
        
        var previousAttendees = museum.Attendees;
        var previousTiles = museum.Tiles;
        
        simulation.RewindMemento();
        Assert.Same(previousAttendees, museum.Attendees);
        Assert.Same(previousTiles, museum.Tiles);
    }
    
    [Fact]
    public void Museum_RewindWithValidPreviousMemento_RewindCorrect()
    {
        var museum = new Museum();
        museum.Attendees = new List<IAttendee> { new Artist(new Coords(0, 0), 0, 0) };
        museum.Tiles = new List<ITile> { new Tile(new Coords(0, 0), new NullColorBehaviorStrategy()) };

        MuseumSimulationFacade simulation = new(museum);
        
        var previousAttendees = museum.Attendees;
        var previousTiles = museum.Tiles;

        simulation.CreateMemento(null);
        simulation.RewindMemento();
        
        Assert.NotSame(previousAttendees, museum.Attendees);
        Assert.NotSame(previousTiles, museum.Tiles);
    }
}