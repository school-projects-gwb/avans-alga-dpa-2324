using System.Runtime.InteropServices.ComTypes;
using BroadwayBB.Common.Behaviors.Interfaces;

namespace BroadwayBB.Common.Entities.Interfaces;

public interface ITile
{
    public int PosX { get; }
    public int PosY { get; }
    
    public IColorBehaviorStrategy ColorBehaviorStrategy { get; }

    public void UpdateColorBehavior(IColorBehaviorStrategy newBehaviorStrategy);
    
    public ITile DeepCopy();
}