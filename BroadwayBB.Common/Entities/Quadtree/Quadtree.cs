using System.Drawing;
using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Interfaces;

namespace BroadwayBB.Common.Entities.Quadtree;

public class Quadtree<T>
{
    private int maxObjects = 5;
    private int objSize = 0;
    private int maxLevels = 3;

    private int _level;
    private List<T> _objects = new();
    private Rectangle _bounds;
    private Quadtree<T>?[] _nodes = new Quadtree<T>?[4] {null, null, null, null};

    public Quadtree(int level, Rectangle bounds)
    {
        _level = level;
        _bounds = bounds;
    }

    public void Clear()
    {
        _objects.Clear();

        for (int i = 0; i < _nodes.Length; i++)
        {
            if (_nodes[i] == null) continue;
            
            _nodes[i].Clear();
            _nodes[i] = null;
        }
    }

    private void SplitNode()
    {
        int subWidth = (_bounds.Width / 2);
        int subHeight = (_bounds.Height / 2);
        int x = _bounds.X;
        int y = _bounds.Y;
        
        int childWidth1 = subWidth + (_bounds.Width % 2);
        int childWidth2 = subWidth;
        int childHeight1 = subHeight + (_bounds.Height % 2);
        int childHeight2 = subHeight;

        _nodes[0] = new Quadtree<T>(_level + 1, new Rectangle(x + childWidth2, y, childWidth1, childHeight2));
        _nodes[1] = new Quadtree<T>(_level + 1, new Rectangle(x, y, childWidth2, childHeight2));
        _nodes[2] = new Quadtree<T>(_level + 1, new Rectangle(x, y + childHeight2, childWidth2, childHeight1));
        _nodes[3] = new Quadtree<T>(_level + 1, new Rectangle(x + childWidth2, y + childHeight2, childWidth1, childHeight1));
    }

    private int GetIndex(T target)
    {
        int index = -1;
        IAttendee obj = (IAttendee)target;
        double verticalMidpoint = _bounds.X + (_bounds.Width / 2.0);
        double horizontalMidpoint = _bounds.Y + (_bounds.Height / 2.0);

        bool isTop = obj.Movement.GetRoundedGridPosY() < horizontalMidpoint && obj.Movement.GetRoundedGridPosY() + objSize < horizontalMidpoint;
        bool isBottom = obj.Movement.GetRoundedGridPosY() > horizontalMidpoint;

        if (obj.Movement.GetRoundedGridPosX() < verticalMidpoint && obj.Movement.GetRoundedGridPosX() + objSize < verticalMidpoint)
        {
            if (isTop) index = 1;
            if (isBottom) index = 2;
        }
        else if (obj.Movement.GetRoundedGridPosX() > verticalMidpoint)
        {
            if (isTop) index = 0;
            if (isBottom) index = 3;
        }

        return index;
    }

    public void Insert(T obj)
    {
        if (_nodes[0] != null)
        {
            int index = GetIndex(obj);
            if (index != -1)
            {
                _nodes[index]?.Insert(obj);
                return;
            }
        }
        
        _objects.Add(obj);
        
        if (_objects.Count > maxObjects && _level < maxLevels)
        {
            if (_nodes[0] == null) SplitNode();

            int i = 0;
            while (i < _objects.Count)
            {
                int index = GetIndex(_objects[i]);
                if (index != -1)
                {
                    _nodes[index].Insert(_objects[i]);
                    _objects.Remove(_objects[i]);
                }
                else
                {
                    i++;
                }
            }
        }
    }

    public List<Rectangle> GetNodeCoordinates()
    {
        List<Rectangle> coordinates = new List<Rectangle>();
        CollectNodeCoordinates(this, coordinates);
        return coordinates;
    }

    private void CollectNodeCoordinates(Quadtree<T> node, List<Rectangle> coordinates)
    {
        if (node._nodes[0] == null)
        {
            coordinates.Add(node._bounds);
        }
        else
        {
            for (int i = 0; i < node._nodes.Length; i++)
            {
                if (node._nodes[i] != null)
                {
                    CollectNodeCoordinates(node._nodes[i], coordinates);
                }
            }
        }
    }
}
