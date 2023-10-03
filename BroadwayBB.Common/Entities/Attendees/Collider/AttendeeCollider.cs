using System.Drawing;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public class AttendeeCollider : IConfigObserver
{
    private List<IAttendee> _attendees = new();
    private Dictionary<StrategyType, IColliderStrategy> _strategies;
    private IColliderStrategy _activeStrategy;

    public AttendeeCollider(Rectangle simulationSize)
    {
        _strategies = new Dictionary<StrategyType, IColliderStrategy>
        {
            { StrategyType.QuadTree, new QuadtreeColliderStrategy(simulationSize) },
            { StrategyType.Naive, new NaiveColliderStrategy(simulationSize)}
        };

        _activeStrategy = _strategies.First().Value;
    } 

    public void SetAttendees(List<IAttendee> attendees) => _attendees = attendees;
    
    public void HandleCollision() => _activeStrategy.HandleCollision(_attendees);

    public List<Rectangle> GetDebugInfo() => _activeStrategy.GetDebugInfo();
    
    public void SetStrategy(StrategyType strategyType) => _activeStrategy = _strategies[strategyType];
    
    public void OnUpdate(ConfigType type, bool value)
    {
        Console.WriteLine(type);
    }
}

public enum StrategyType
{
    QuadTree, Naive
}