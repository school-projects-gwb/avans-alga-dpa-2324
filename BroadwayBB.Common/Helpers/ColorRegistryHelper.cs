using BroadwayBB.Common.Behaviors;

namespace BroadwayBB.Common.Helpers;


public sealed class ColorRegistryHelper
{
    private static readonly Lazy<ColorRegistryHelper> Instance = new(() => new ColorRegistryHelper());

    private readonly Dictionary<ColorName, RgbColor> _colorMap = new();
    private readonly Dictionary<char, ColorName> _colorNameMap = new();

    private ColorRegistryHelper()
    {
        RegisterColorName('R', ColorName.Red);
        RegisterColor(ColorName.Red, new RgbColor(255, 107, 107));

        RegisterColorName('G', ColorName.Grey);
        RegisterColor(ColorName.Grey, new RgbColor(176, 176, 176));

        RegisterColorName('Y', ColorName.Yellow);
        RegisterColor(ColorName.Yellow, new RgbColor(255, 255, 153));

        RegisterColorName('B', ColorName.Blue);
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

    public ColorName GetColorName(char tag)
    {
        lock (_colorNameMap)
            return _colorNameMap.TryGetValue(tag, out var colorName) ? colorName : ColorName.White;
    }

    public Dictionary<ColorName, RgbColor> GetAllColors()
    {
        lock (_colorMap)
            return _colorMap;
    }

    public void RegisterColor(ColorName colorName, RgbColor color)
    {
        lock (_colorMap)
            _colorMap[colorName] = color;
    }

    public void RegisterColorName(char tag, ColorName colorName)
    {
        lock (_colorNameMap)
            _colorNameMap[tag] = colorName;
    }
}
