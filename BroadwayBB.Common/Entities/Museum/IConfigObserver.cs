namespace BroadwayBB.Common.Entities.Museum;

public interface IConfigObserver
{
    public void OnUpdate(ConfigType type, bool value);
}