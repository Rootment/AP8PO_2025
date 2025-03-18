using System.Diagnostics.CodeAnalysis;

namespace Snake;

/// <summary>
/// Struct representing pixel in console
/// </summary>
public struct Pixel : IEquatable<Pixel>
{
    public Pixel (int xPos, int yPos, ConsoleColor color)
    {
        XPos = xPos;
        YPos = yPos;
        ScreenColor = color;
    }
    
    public int XPos { get; set; }
    public int YPos { get; set; }
    public ConsoleColor ScreenColor { get; set; }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return base.Equals(obj);
    }

    public bool Equals(Pixel other)
    {
        return XPos == other.XPos && YPos == other.YPos;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(XPos, YPos);
    }
}