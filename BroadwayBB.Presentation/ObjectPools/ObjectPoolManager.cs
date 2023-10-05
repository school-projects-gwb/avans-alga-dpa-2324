using System.Collections.Generic;
using Avalonia.Controls.Shapes;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Helpers;

namespace BroadwayBB.Presentation.ObjectPools;

public class ObjectPoolManager
{
    public IObjectPool<Rectangle> AttendeeObjectPool { get; private set; }
    public IObjectPool<Rectangle> TileObjectPool { get; private set; }

    public void CreateAttendeeObjectPool(int poolAmount, double width, double height)
    {
        var config = new ObjectPoolConfiguration
        {
            MaxPoolAmount = poolAmount,
            SupportedColors = new Dictionary<ColorName, RgbColor>
            {
                { ColorName.Black, ColorRegistryHelper.GetInstance.GetColor(ColorName.Black) },
                { ColorName.Red, ColorRegistryHelper.GetInstance.GetColor(ColorName.Red) }
            },
            ObjectWidth = width,
            ObjectHeight = height
        };
        
        AttendeeObjectPool = new CanvasItemPool(config);
    }
    
    public void CreateTileObjectPool(int poolAmount, double width, double height)
    {
        var config = new ObjectPoolConfiguration
        {
            MaxPoolAmount = poolAmount, 
            SupportedColors = ColorRegistryHelper.GetInstance.GetAllColors(),
            ObjectWidth = width,
            ObjectHeight = height
        };
        
        TileObjectPool = new CanvasItemPool(config);
    }
}