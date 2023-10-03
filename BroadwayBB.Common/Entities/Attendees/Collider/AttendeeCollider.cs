using System.Drawing;
using BroadwayBB.Common.Entities.Museum;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public class AttendeeCollider : IConfigObserver
{
    private List<IAttendee> _attendees = new();
    private Dictionary<ColliderStrategyType, IColliderStrategy> _strategies;
    private ColliderStrategyType _activeStrategyType;

    public AttendeeCollider(Rectangle simulationSize)
    {
        _strategies = new Dictionary<ColliderStrategyType, IColliderStrategy>
        {
            { ColliderStrategyType.QuadTree, new QuadtreeColliderStrategy(simulationSize) },
            { ColliderStrategyType.Naive, new NaiveColliderStrategy(simulationSize)}
        };

        _activeStrategyType = ColliderStrategyType.QuadTree;
    } 

    public void SetAttendees(List<IAttendee> attendees) => _attendees = attendees;
    
    public void HandleCollision() => _strategies[_activeStrategyType].HandleCollision(_attendees);

    public List<Rectangle> GetDebugInfo() => _strategies[_activeStrategyType].GetDebugInfo();
    
    public void OnUpdate(ConfigType type, bool value)
    {
        if (type == ConfigType.IsQuadtreeCollision) SetNextStrategy();
    }
    
    private void SetNextStrategy()
    {
        if (!_strategies.ContainsKey(_activeStrategyType))
        {
            _activeStrategyType = _strategies.Keys.First();
            return;
        }
    
        var currentIndex = Array.IndexOf(_strategies.Keys.ToArray(), _activeStrategyType);
        var nextIndex = (currentIndex + 1) % _strategies.Count;
        _activeStrategyType = _strategies.Keys.ToArray()[nextIndex];
    }
}