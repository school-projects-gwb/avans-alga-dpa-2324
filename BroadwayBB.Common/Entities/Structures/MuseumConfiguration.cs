using System.Dynamic;

namespace BroadwayBB.Common.Entities.Structures;

public class MuseumConfiguration
{
    private readonly List<IConfigObserver> _observers = new();
    
    public Dictionary<ConfigType, bool> Config = new()
    {
        { ConfigType.ShouldRenderAttendees, true },
        { ConfigType.ShouldMoveAttendees, false },
        { ConfigType.ShouldRenderQuadtree, false },
        { ConfigType.IsQuadtreeCollision, true }
    };

    public void Toggle(ConfigType type)
    {
        Config[type] = !Config[type];
        NotifyUpdated(type);
    } 
    
    public bool Get(ConfigType type) => Config[type];

    public void AddObserver(IConfigObserver observer) => _observers.Add(observer);

    private void NotifyUpdated(ConfigType type) => _observers.ForEach(observer => observer.OnUpdate(type, Get(type)));
}