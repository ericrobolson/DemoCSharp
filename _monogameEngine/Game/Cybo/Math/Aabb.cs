using System;

namespace Cybo;

public struct Aabb : IEquatable<Aabb>
{
    /// <summary>
    /// The position of the AABB.
    /// </summary>
    /// <value></value>
    public Vec2 Position { get; set; }

    /// <summary>
    /// The size of the AABB.
    /// </summary>
    /// <value></value>
    public Vec2 Size { get; set; }

    public Aabb(int x, int y, int w, int h) : this(new Vec2(x, y), new Vec2(w, h)) { }

    public Aabb(Vec2 position, Vec2 size)
    {
        Position = position;
        Size = size;
    }

    public bool Equals(Aabb other)
    {
        return Position.Equals(other.Position) && Size.Equals(other.Size);
    }

    /// <summary>
    /// Returns whether the AABB contains the given vector.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public bool Contains(Vec2 v)
    {
        var max = Position + Size;
        var min = Position;

        return min.X <= v.X && max.X >= v.X && min.Y <= v.Y && max.Y >= v.Y;
    }

    /// <summary>
    /// Returns whether two AABBs overlap
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Overlaps(Aabb other)
    {
        // TODO: OPTIMIZATION
        // This is a suboptimal algorithm I think. Deferring for now.

        var bMin = other.Position;
        var bMax = other.Position + other.Size;

        if (Contains(bMin) || Contains(bMax))
        {
            return true;
        }

        var aMin = Position;
        var aMax = Position + Size;

        return other.Contains(aMin) || other.Contains(aMax);
    }
}