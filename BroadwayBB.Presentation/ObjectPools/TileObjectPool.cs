using System.Collections.Generic;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Helpers;

namespace BroadwayBB.Presentation.ObjectPools;

public class TileObjectPool : IObjectPool
{
    private double _objectWidth, _objectHeight;
    private readonly int _poolAmount = 50; 
    private Dictionary<ColorName, List<Rectangle>> _rectangleObjectPool = new();
    private List<KeyValuePair<ColorName, Rectangle>> _markedForRelease = new();
    private readonly object _poolLock = new();
    
    public TileObjectPool(double objectWidth, double objectHeight, int maxPoolAmount)
    {
        _objectWidth = objectWidth;
        _objectHeight = objectHeight;
        _poolAmount = maxPoolAmount;
        Create();
    }
    
    public void Create()
    {
        var allColors = ColorRegistry.Instance.GetAllColors();

        foreach (var colorMapRecord in allColors)
        {
            var key = colorMapRecord.Key;
            var values = new List<Rectangle>();

            for (int i = 0; i < _poolAmount; i++)
            {
                values.Add(new Rectangle
                {
                    Width = _objectWidth,
                    Height = _objectHeight,
                    Fill = new SolidColorBrush(Color.FromRgb(colorMapRecord.Value.Red, colorMapRecord.Value.Green, colorMapRecord.Value.Blue)),
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                });
            }
            
            lock (_poolLock) _rectangleObjectPool[key] = values;
        }
    }

    public Rectangle? GetObject(ColorName colorName)
    {
        lock (_poolLock)
        {
            if (!_rectangleObjectPool.TryGetValue(colorName, out List<Rectangle> pool)) return null;
            if (pool.Count <= 0) return null;
            var obj = pool[0];
            pool.RemoveAt(0);
            return obj;
        }
    }

    public void MarkForRelease(ColorName key, Rectangle rectangle)
    {
        lock (_poolLock)
            _markedForRelease.Add(new KeyValuePair<ColorName, Rectangle>(key, rectangle));
    }

    public void ReleaseMarked()
    {
        lock (_poolLock)
        {
            foreach (var marked in _markedForRelease)
                if (_rectangleObjectPool.TryGetValue(marked.Key, out List<Rectangle> pool)) pool.Add(marked.Value);
            
            _markedForRelease.Clear();
        }
    }
}