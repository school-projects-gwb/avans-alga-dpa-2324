namespace BroadwayBB.Common.Entities.Museum;

public class MuseumConfiguration
{
    private readonly List<IConfigObserver> _observers = new();
    
    private readonly Dictionary<ConfigType, bool> _config = new()
    {
        { ConfigType.ShouldRenderAttendees, true },
        { ConfigType.ShouldMoveAttendees, false },
        { ConfigType.ShouldHaveTileBehavior, true },
        { ConfigType.ShouldRenderQuadtree, false },
        { ConfigType.IsQuadtreeCollision, true },
    };

    public void Toggle(ConfigType type)
    {
        _config[type] = !_config[type];
        NotifyUpdated(type);
    } 
    
    public bool Get(ConfigType type) => _config[type];

    public void AddObserver(IConfigObserver observer) => _observers.Add(observer);

    private void NotifyUpdated(ConfigType type) => _observers.ForEach(observer => observer.OnUpdate(type, Get(type)));
}