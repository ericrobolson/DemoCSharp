using System;

namespace Cybo.Ecs;

public struct EntityId : IEquatable<EntityId>
{
    private ulong _id;

    /// <summary>
    /// Returns the next EntityId in the series.
    /// </summary>
    /// <returns></returns>
    public EntityId Next()
    {
        return new EntityId { _id = this._id + 1 };
    }

    public bool Equals(EntityId other)
    {
        return _id == other._id;
    }


    public static bool operator ==(EntityId lhs, EntityId rhs)
    {
        return lhs.Equals(rhs);
    }
    public static bool operator !=(EntityId lhs, EntityId rhs)
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
        return _id.GetHashCode();
    }
}