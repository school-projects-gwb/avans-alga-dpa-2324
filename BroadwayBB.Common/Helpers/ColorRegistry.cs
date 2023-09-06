using BroadwayBB.Common.Behaviors;

namespace BroadwayBB.Common.Helpers;


public sealed class ColorRegistry
{
    private static readonly Lazy<ColorRegistry> _instance = new(() => new ColorRegistry());

    private readonly Dictionary<ColorName, RGBColor> _colorMap = new();

    private ColorRegistry()
    {
        RegisterColor(ColorName.Red, new RGBColor(255, 107, 107));
        RegisterColor(ColorName.Grey, new RGBColor(176, 176, 176));
        RegisterColor(ColorName.Yellow, new RGBColor(255, 255, 153));
        RegisterColor(ColorName.Blue, new RGBColor(153, 204, 255));
        RegisterColor(ColorName.White, new RGBColor(255, 255, 255));
        RegisterColor(ColorName.Black, new RGBColor(0, 0, 0));
    }

    public static ColorRegistry Instance => _instance.Value;

    public RGBColor GetColor(ColorName colorName)
    {
        lock (_colorMap)
            return _colorMap.TryGetValue(colorName, out var rgbValue) ? rgbValue : new RGBColor(0, 0, 0);
    }

    public Dictionary<ColorName, RGBColor> GetAllColors()
    {
        lock (_colorMap)
            return _colorMap;
    }

    public void RegisterColor(ColorName colorName, RGBColor rgbValue)
    {
        lock (_colorMap)
            _colorMap[colorName] = rgbValue;
    }
}
