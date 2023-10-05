using System.Collections.Generic;
using Avalonia.Controls.Shapes;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Helpers;

namespace BroadwayBB.Presentation.ObjectPools;

public class ObjectPoolManager
{
    public IObjectPool<Rectangle> AttendeeObjectPool { get; private set; }
    public IObjectPool<Rectangle> TileObjectPool { get; private set; }
    public IObjectPool<Rectangle> DebugObjectPool { get; private set; }

    public void CreateAttendeeObjectPool(int poolAmount, Coords tileSize)
    {
        var config = new ObjectPoolConfiguration
        {
            MaxPoolAmount = poolAmount,
            SupportedColors = new Dictionary<ColorName, RgbColor>
            {
                { ColorName.Black, ColorRegistryHelper.GetInstance.GetColor(ColorName.Black) },
                { ColorName.Red, ColorRegistryHelper.GetInstance.GetColor(ColorName.Red) }
            },
            ObjectWidth = tileSize.Xd,
            ObjectHeight = tileSize.Yd
        };
        
        AttendeeObjectPool = new CanvasItemPool(config);
    }
    
    public void CreateTileObjectPool(int poolAmount, Coords tileSize)
    {
        var config = new ObjectPoolConfiguration
        {
            MaxPoolAmount = poolAmount, 
            SupportedColors = ColorRegistryHelper.GetInstance.GetAllColors(),
            ObjectWidth = tileSize.Xd,
            ObjectHeight = tileSize.Yd
        };
        
        TileObjectPool = new CanvasItemPool(config);
    }
    
    public void CreateDebugObjectPool(int poolAmount, Coords tileSize)
    {
        var config = new ObjectPoolConfiguration
        {
            MaxPoolAmount = poolAmount, 
            SupportedColors = new Dictionary<ColorName, RgbColor>
            {
                { ColorName.Black, ColorRegistryHelper.GetInstance.GetColor(ColorName.Black) },
            },
            ObjectWidth = tileSize.Xd,
            ObjectHeight = tileSize.Yd,
            BorderOnly = true
        };
        
        DebugObjectPool = new CanvasItemPool(config);
    }
}