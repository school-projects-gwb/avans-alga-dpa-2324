using System.Drawing;
using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Attendees.Collider;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees;

public class AttendeeManager
{
    private readonly List<IAttendee> _markedForRemoval = new();
    private readonly List<IAttendee> _markedForCreation = new();
    private readonly int _markedLimit = 5, _attendeeHardLimit = 100;
    private readonly double _minSpeed = 1.0, _maxSpeed = 3.0;
    private readonly Random _random = new();

    public AttendeeCollider AttendeeCollider { get; private set; }
    public int AttendeeLimit { get; private set; } = 50;

    private List<IAttendee> _attendees = new();

    public List<IAttendee> Attendees
    {
        get => _attendees;
        set
        {
            _attendees = value;
            AttendeeCollider?.SetAttendees(value);
        }
    }

    public void HandleCollision() => AttendeeCollider.HandleCollision();

    public void InitCollider(int width, int height)
    {
        AttendeeCollider = new AttendeeCollider(new Rectangle(0, 0, width, height));
    }
    
    public List<DebugTile> GetColliderDebugInfo() => AttendeeCollider.GetDebugInfo();

    public void HandleTileCollisionResult(TileCollisionResult tileCollisionResult, IAttendee targetAttendee)
    {
        if (tileCollisionResult.ShouldCreateArtist) CreateArtist(targetAttendee.Movement.GridPos);
        if (tileCollisionResult.ShouldRemoveArtist) RemoveArtist(targetAttendee);
        if (tileCollisionResult.IsInPath) targetAttendee.Movement.IsColliding = true;
    }

    private void CreateArtist(Coords targetPos)
    {
        if (Attendees.Count >= AttendeeLimit) return;
        var hasVerticalSpeed = _random.Next(2) == 1; // 50% chance
        var speed = Math.Clamp(_random.NextDouble() * (_maxSpeed - _minSpeed) + _minSpeed, _minSpeed, _maxSpeed);

        var newArtist = new Artist(targetPos, hasVerticalSpeed ? speed : 0, hasVerticalSpeed ? 0 : speed, MovementDirectionExtensions.GetRandomDirection());
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

    public List<IAttendee> CreateMemento() { 
        lock(Attendees) return Attendees.Select(attendee => attendee.DeepCopy()).ToList();  
    }
}