using System.Drawing;

namespace BroadwayBB.Common.Helpers;

public class RgbColor
{
    public byte Red { get; }
    public byte Green { get; }
    public byte Blue { get; }

    public RgbColor(byte red, byte green, byte blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }

    public RgbColor(Color color)
    {
        Red   = color.R;
        Green = color.G;
        Blue  = color.B;
    }
}