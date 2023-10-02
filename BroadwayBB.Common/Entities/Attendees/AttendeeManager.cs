using System.Drawing;
using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Quadtree;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Attendees;

public class AttendeeManager
{
    public List<IAttendee> Attendees { get; set; } = new();
    public Quadtree<IAttendee> AttendeeQuadtree;
    
    private readonly List<IAttendee> _markedForRemoval = new();
    private readonly List<IAttendee> _markedForCreation = new();
    private readonly int _markedLimit = 5, _attendeeHardLimit = 100;
    private readonly double _minSpeed = 1.0, _maxSpeed = 3.0;
    private readonly Random _random = new();
    public int AttendeeLimit { get; private set; } = 50;
    
    public void HandleCollision(IAttendee attendee)
    {
        // todo implement
    }

    public void InitQuadtree(int width, int height)
    {
        AttendeeQuadtree = new Quadtree<IAttendee>(0, new Rectangle(0, 0, width, height));
    }
    
    public void HandleTileCollisionResult(TileCollisionResult tileCollisionResult, IAttendee targetAttendee)
    {
        if (tileCollisionResult.ShouldCreateArtist) 
            CreateArtist(targetAttendee.Movement.GetRoundedGridPosX(), targetAttendee.Movement.GetRoundedGridPosY());
        
        if (tileCollisionResult.ShouldRemoveArtist)
            RemoveArtist(targetAttendee);
    }
    
    private void CreateArtist(int targetPosX, int targetPosY)
    {
        if (Attendees.Count >= AttendeeLimit) return;
        var hasVerticalSpeed = _random.Next(2) == 1; // 50% chance
        var speed = Math.Clamp(_random.NextDouble() * (_maxSpeed - _minSpeed) + _minSpeed, _minSpeed, _maxSpeed);

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
        _markedForCreation.ForEach(attendee =>
        {
            if (Attendees.Count >= AttendeeLimit) return;
            Attendees.Add(attendee);
        });
        
        _markedForCreation.Clear();
    }
    
    public void SetAttendeeLimit(int limit) => AttendeeLimit = limit <= _attendeeHardLimit ? limit : _attendeeHardLimit;

    public List<IAttendee> CreateMemento() => Attendees.Select(attendee => attendee.DeepCopy()).ToList();
}