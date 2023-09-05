using System.Dynamic;
using BroadwayBB.Common.Behaviors;

namespace BroadwayBB.Presentation.ObjectPools;

public interface IObjectPool<T>
{
    public T? GetObject(ColorName key);
    public void ReleaseMarked();
    public void MarkForRelease(ColorName key, T obj);
}