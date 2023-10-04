using System.Drawing;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities.Extensions;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Quadtree;

public class Quadtree<T>
{
    private readonly int _maxObjectsBeforeSplit = 10;
    private readonly int _objectSize = 0;
    private readonly int _maxTreeDepth = 10;

    private readonly int _currentTreeDepth;
    private readonly List<TreeObject<T>> _objects = new();
    private readonly Rectangle _bounds;
    private readonly Quadtree<T>?[] _nodes = new Quadtree<T>?[4] {null, null, null, null};

    public Quadtree(int currentTreeDepth, Rectangle bounds)
    {
        _currentTreeDepth = currentTreeDepth;
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

        _nodes[0] = new Quadtree<T>(_currentTreeDepth + 1, new Rectangle(x + childWidth2, y, childWidth1, childHeight2));
        _nodes[1] = new Quadtree<T>(_currentTreeDepth + 1, new Rectangle(x, y, childWidth2, childHeight2));
        _nodes[2] = new Quadtree<T>(_currentTreeDepth + 1, new Rectangle(x, y + childHeight2, childWidth2, childHeight1));
        _nodes[3] = new Quadtree<T>(_currentTreeDepth + 1, new Rectangle(x + childWidth2, y + childHeight2, childWidth1, childHeight1));
    }

    private int GetIndex(TreeObject<T> target)
    {
        var index = -1;
        TreeObject<T> obj = target;
        double verticalMidpoint = _bounds.X + (_bounds.Width / 2.0);
        double horizontalMidpoint = _bounds.Y + (_bounds.Height / 2.0);

        bool isTop = obj.PosY < horizontalMidpoint && obj.PosY + _objectSize < horizontalMidpoint;
        bool isBottom = obj.PosY > horizontalMidpoint;

        if (obj.PosX < verticalMidpoint && obj.PosX + _objectSize < verticalMidpoint)
        {
            if (isTop) index = 1;
            if (isBottom) index = 2;
        }
        else if (obj.PosX > verticalMidpoint)
        {
            if (isTop) index = 0;
            if (isBottom) index = 3;
        }

        return index;
    }

    private void Insert(TreeObject<T> obj) => Insert(obj.Object, obj.PosX, obj.PosY);
    
    public void Insert(T baseObject, int objectPosX, int objectPosY)
    {
        TreeObject<T> obj = new TreeObject<T>(baseObject, objectPosX, objectPosY);
        
        if (_nodes[0] != null)
        {
            var index = GetIndex(obj);
            if (index != -1)
            {
                _nodes[index]?.Insert(obj);
                return;
            }
        }
        
        _objects.Add(obj);

        if (_objects.Count < _maxObjectsBeforeSplit || _currentTreeDepth >= _maxTreeDepth) return;
        
        if (_nodes[0] == null) SplitNode();

        var i = 0;
        while (i < _objects.Count)
        {
            int index = GetIndex(_objects[i]);
            if (index != -1)
            {
                _nodes[index].Insert(_objects[i]);
                _objects.Remove(_objects[i]);
                continue;
            }
            
            i++;
        }
    }

    public void GetObjectsInQuadrant(List<T> result, TreeObject<T> targetObject)
    {
        var index = GetIndex(targetObject);
        if (index != -1 && _nodes[0] != null) _nodes[index].GetObjectsInQuadrant(result, targetObject);
    
        result.AddRange(_objects
            .Where(obj => GetIndex(obj) == index && !obj.Object.Equals(targetObject.Object))
            .Select(obj => obj.Object));
    }
    
    public List<DebugTile> GetNodeCoordinates()
    {
        List<DebugTile> coordinates = new();
        CollectNodeCoordinates(this, coordinates);
        return coordinates;
    }

    private void CollectNodeCoordinates(Quadtree<T> node, List<DebugTile> coordinates)
    {
        if (node._nodes[0] == null)
            coordinates.Add(new DebugTile { PositionInfo = node._bounds, ColorName = ColorName.Red });
        else
            foreach (var obj in node._nodes)
                if (obj != null) CollectNodeCoordinates(obj, coordinates);
    }
}
