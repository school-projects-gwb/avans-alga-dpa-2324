namespace BroadwayBB.Common.Entities.Quadtree;

public struct TreeObject<T>
{
    public T Object { get; }
    public int PosX { get; }
    public int PosY { get; }

    public TreeObject(T obj, int posX, int posY)
    {
        Object = obj;
        PosX = posX;
        PosY = posY;
    }
}