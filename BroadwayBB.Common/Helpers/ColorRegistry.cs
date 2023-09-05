using BroadwayBB.Common.Behaviors;

namespace BroadwayBB.Common.Helpers;


public sealed class ColorRegistry
{
    private static readonly Lazy<ColorRegistry> instance = new(() => new ColorRegistry());

    private readonly Dictionary<ColorName, RGBColor> colorMap = new();

    private ColorRegistry()
    {
        RegisterColor(ColorName.Red, new RGBColor(255, 0, 0));
        RegisterColor(ColorName.Grey, new RGBColor(128, 128, 128));
        RegisterColor(ColorName.Yellow, new RGBColor(255, 255, 0));
        RegisterColor(ColorName.Blue, new RGBColor(0, 0, 255));
        RegisterColor(ColorName.White, new RGBColor(255, 255, 255));
    }

    public static ColorRegistry Instance => instance.Value;

    public RGBColor GetColor(ColorName colorName)
    {
        lock (colorMap)
            return colorMap.TryGetValue(colorName, out var rgbValue) ? rgbValue : new RGBColor(0, 0, 0);
    }

    public Dictionary<ColorName, RGBColor> GetAllColors()
    {
        lock (colorMap)
            return colorMap;
    }

    public void RegisterColor(ColorName colorName, RGBColor rgbValue)
    {
        lock (colorMap)
            colorMap[colorName] = rgbValue;
    }
}
