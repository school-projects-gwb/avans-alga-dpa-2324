using System.Collections.Generic;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Helpers;

namespace BroadwayBB.Presentation.ObjectPools;

public class ObjectPoolConfiguration
{
    public double ObjectWidth;
    public double ObjectHeight;
    public int MaxPoolAmount;
    public Dictionary<ColorName, RgbColor> SupportedColors;
}