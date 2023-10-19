using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Common.Entities.Quadtree;

public struct TreeObject<T>
{
    public T Object { get; }
    public Coords Pos { get; }

    public TreeObject(T obj, Coords pos)
    {
        Object = obj;
        Pos = pos;
    }
}