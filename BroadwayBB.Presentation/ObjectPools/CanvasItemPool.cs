using System.Collections.Generic;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Helpers;

namespace BroadwayBB.Presentation.ObjectPools;

public class CanvasItemPool : IObjectPool<Rectangle>
{
    private readonly ObjectPoolConfiguration _configuration;
    private readonly Dictionary<ColorName, List<Rectangle>> _rectangleObjectPool = new();
    private readonly List<KeyValuePair<ColorName, Rectangle>> _markedForRelease = new();
    private readonly object _poolLock = new();
    
    public CanvasItemPool(ObjectPoolConfiguration configuration)
    {
        _configuration = configuration;
        Create();
    }
    
    private void Create()
    {
        foreach (var colorMapRecord in _configuration.SupportedColors)
        {
            var key = colorMapRecord.Key;
            var values = new List<Rectangle>();

            for (int i = 0; i < _configuration.MaxPoolAmount; i++)
            {
                var rect = new Rectangle
                {
                    Width = _configuration.ObjectWidth,
                    Height = _configuration.ObjectHeight,
                };

                var color =  new SolidColorBrush(Color.FromRgb(colorMapRecord.Value.Red, colorMapRecord.Value.Green,
                    colorMapRecord.Value.Blue));

                if (_configuration.BorderOnly)
                {
                    rect.Stroke = color;
                    rect.StrokeThickness = 1;
                }
                else
                {
                    rect.Fill = color;   
                }
                
                values.Add(rect);
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