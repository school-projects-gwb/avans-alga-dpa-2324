using BroadwayBB.Common.Behaviors;

namespace BroadwayBB.Common.Helpers;


public sealed class ColorRegistryHelper
{
    private static readonly Lazy<ColorRegistryHelper> Instance = new(() => new ColorRegistryHelper());

    private readonly Dictionary<ColorName, RgbColor> _colorMap = new();

    private ColorRegistryHelper()
    {
        RegisterColor(ColorName.Red, new RgbColor(255, 107, 107));
        RegisterColor(ColorName.Grey, new RgbColor(176, 176, 176));
        RegisterColor(ColorName.Yellow, new RgbColor(255, 255, 153));
        RegisterColor(ColorName.Blue, new RgbColor(153, 204, 255));
        RegisterColor(ColorName.White, new RgbColor(255, 255, 255));
        RegisterColor(ColorName.Black, new RgbColor(0, 0, 0));
    }

    public static ColorRegistryHelper GetInstance => Instance.Value;

    public RgbColor GetColor(ColorName colorName)
    {
        lock (_colorMap)
            return _colorMap.TryGetValue(colorName, out var rgbValue) ? rgbValue : new RgbColor(0, 0, 0);
    }

    public Dictionary<ColorName, RgbColor> GetAllColors()
    {
        lock (_colorMap)
            return _colorMap;
    }

    public void RegisterColor(ColorName colorName, RgbColor rgbValue)
    {
        lock (_colorMap)
            _colorMap[colorName] = rgbValue;
    }
}
