using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities;

public class AttendeeManager
{
    public List<IAttendee> Attendees { get; set; }
    
    private List<IAttendee> _markedForRemoval = new();
    private List<IAttendee> _markedForCreation = new();
    private readonly int _markedLimit = 5;
    
    public void HandleTileCollisionResult(TileCollisionResult tileCollisionResult, IAttendee targetAttendee)
    {
        if (tileCollisionResult.ShouldCreateArtist) 
            CreateArtist((int) Math.Floor(targetAttendee.Movement.GridPosX), (int) Math.Floor(targetAttendee.Movement.GridPosY));
        
        if (tileCollisionResult.ShouldRemoveArtist)
            RemoveArtist(targetAttendee);
    }

    private void CreateArtist(int targetPosX, int targetPosY)
    {
        if (_markedForCreation.Count > _markedLimit) return;
        var random = new Random();
        var hasVerticalSpeed = random.Next(2) == 1; // 50% chance
        double minSpeed = 1.0, maxSpeed = 3.0;
        var speed = Math.Clamp(random.NextDouble() * (maxSpeed - minSpeed) + minSpeed, minSpeed, maxSpeed);
        
        var newArtist = new Artist(targetPosX, targetPosY, hasVerticalSpeed ? speed : 0, hasVerticalSpeed ? 0 : speed);
        _markedForCreation.Add(newArtist);
    }

    private void RemoveArtist(IAttendee removalTarget)
    {
        if (_markedForRemoval.Count > _markedLimit) return;
        _markedForRemoval.Add(removalTarget);  
    } 

    public void HandleAttendeeQueue()
    {
        RemoveMarkedAttendees();
        AddMarkedAttendees();
    }
    
    private void RemoveMarkedAttendees()
    {
        _markedForRemoval.ForEach(attendee => Attendees.Remove(attendee));
        _markedForRemoval.Clear();
    }

    private void AddMarkedAttendees()
    {
        _markedForCreation.ForEach(attendee => Attendees.Add(attendee));
        _markedForCreation.Clear();
    }
}