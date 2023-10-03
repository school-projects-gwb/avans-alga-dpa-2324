using System.Runtime.CompilerServices;

namespace BroadwayBB.Common.Entities.Structures;

public interface IConfigObserver
{
    public void OnUpdate(ConfigType type, bool value);
}