namespace Cybo;

public struct Color
{
    public Color() : this(255, 255, 255)
    { }

    public Color(byte r, byte g, byte b) : this(r, g, b, 255) { }

    public Color(byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public byte R { get; }
    public byte G { get; }
    public byte B { get; }
    public byte A { get; }

    public static Color Blue()
    {
        return new Color(0, 0, 255);
    }
    public static Color Green()
    {
        return new Color(0, 255, 0);
    }
    public static Color Red()
    {
        return new Color(255, 0, 0);
    }
    public static Color White()
    {
        return new Color(255, 255, 255);
    }
}