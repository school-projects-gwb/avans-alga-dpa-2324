using System.Dynamic;
using BroadwayBB.Common.Behaviors;

namespace BroadwayBB.Presentation.ObjectPools;

public interface IObjectPool<T>
{
    public void Create();
    public void MarkForRelease(ColorName key, T item);
}