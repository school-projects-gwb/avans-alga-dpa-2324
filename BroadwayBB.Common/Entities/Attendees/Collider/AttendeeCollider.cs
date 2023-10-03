using System.Drawing;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public class AttendeeCollider : IConfigObserver
{
    private List<IAttendee> _attendees = new();
    private Dictionary<StrategyType, IColliderStrategy> _strategies;
    private StrategyType _activeStrategyType;

    public AttendeeCollider(Rectangle simulationSize)
    {
        _strategies = new Dictionary<StrategyType, IColliderStrategy>
        {
            { StrategyType.QuadTree, new QuadtreeColliderStrategy(simulationSize) },
            { StrategyType.Naive, new NaiveColliderStrategy(simulationSize)}
        };

        _activeStrategyType = StrategyType.QuadTree;
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

public enum StrategyType
{
    QuadTree, Naive
}