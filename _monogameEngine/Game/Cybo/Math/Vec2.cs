using System;
using System.Runtime.Serialization;

namespace Cybo;

[DataContract]
public struct Vec2 : IEquatable<Vec2>
{
    [DataMember]
    public Int32 X { get; set; }

    [DataMember]
    public Int32 Y { get; set; }

    public Vec2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Vec2 other)
    {
        return X == other.X && Y == other.Y;
    }

    public static Vec2 operator +(Vec2 lhs, Vec2 rhs)
    {
        return new Vec2(lhs.X + rhs.X, lhs.Y + rhs.Y);
    }
    public static Vec2 operator -(Vec2 lhs, Vec2 rhs)
    {
        return new Vec2(lhs.X - rhs.X, lhs.Y - rhs.Y);
    }
    public static Vec2 operator -(Vec2 value)
    {
        return new Vec2(-value.X, -value.Y);
    }
    public static Vec2 operator *(Vec2 lhs, Vec2 rhs)
    {
        return new Vec2(lhs.X * rhs.X, lhs.Y * rhs.Y);
    }
    public static Vec2 operator *(Vec2 lhs, int rhs)
    {
        return new Vec2(lhs.X * rhs, lhs.Y * rhs);
    }
    public static Vec2 operator /(Vec2 lhs, Vec2 rhs)
    {
        return new Vec2(lhs.X / rhs.X, lhs.Y / rhs.Y);
    }
    public static Vec2 operator /(Vec2 lhs, int rhs)
    {
        return new Vec2(lhs.X / rhs, lhs.Y / rhs);
    }
    public static Vec2 operator *(int lhs, Vec2 rhs)
    {
        return new Vec2(lhs * rhs.X, lhs * rhs.Y);
    }
    public static bool operator ==(Vec2 lhs, Vec2 rhs)
    {
        return lhs.Equals(rhs);
    }
    public static bool operator !=(Vec2 lhs, Vec2 rhs)
    {
        return !lhs.Equals(rhs);
    }

    public override bool Equals(object? obj)
    {
        if (obj != null && obj is Vec2)
        {
            return Equals((Vec2)obj);
        }

        return false;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (X.GetHashCode() * 397) ^ Y.GetHashCode();
        }
    }
}

