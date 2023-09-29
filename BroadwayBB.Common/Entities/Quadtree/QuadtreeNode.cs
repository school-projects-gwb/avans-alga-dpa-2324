using System.Drawing;

namespace BroadwayBB.Common.Entities.Quadtree;

public class QuadtreeNode<T>
{
    public Rectangle Bounds { get; }
    public List<T> Objects { get; }
    public QuadtreeNode<T>[] Children { get; }

    public QuadtreeNode(Rectangle bounds)
    {
        Bounds = bounds;
        Objects = new List<T>();
        Children = new QuadtreeNode<T>[4];
    }
}