using System;
using System.Collections.Generic;
using Cybo.Containers;

namespace Cybo.Ecs;

internal interface IEntityStorage
{
    /// <summary>
    /// Deletes the value for the given entity.
    /// </summary>
    /// <param name="id"></param>
    void Delete(EntityId id);

    /// <summary>
    /// Resets the storage.
    /// </summary>
    void Reset();
}

/// <summary>
/// A simplified component store objects keyed by Entity Ids.
/// </summary>
/// <typeparam name="T"></typeparam>
internal class EntityStore<T> : IEntityStorage where T : new()
{
    public StaticList<EntityId> Entities;
    public StaticList<T> Values;
    private int _activeCount;
    private int _capacity;


    /// <summary>
    /// Creates a new component store.
    /// </summary>
    /// <param name="capacity"></param>
    public EntityStore(int capacity)
    {
        Entities = new StaticList<EntityId>(capacity);
        Values = new StaticList<T>(capacity);
        _capacity = capacity;
    }

    /// <summary>
    /// Adds the given component to the entity.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Add(EntityId id, out int idx)
    {
        // Return existing entity if it exists.
        var exists = GetIdx(id, out idx);

        // Create new entity if space exists.
        if (!exists && _activeCount < _capacity)
        {
            Entities[_activeCount] = id;
            idx = _activeCount;
            _activeCount += 1;
            exists = true;
        }

        return exists;
    }

    /// <summary>
    /// Returns the active list of components.
    /// </summary>
    /// <returns></returns>
    public List<T> ActiveComponents()
    {
        return Values.GetRange(0, _activeCount);
    }

    /// <summary>
    /// Performs an operation on on all active entity/component pairs.
    /// </summary>
    /// <param name="operation"></param>
    public void ForEach(Action<EntityId, T> operation)
    {
        for (int i = 0; i < _activeCount; i++)
        {
            operation(Entities[i], Values[i]);
        }
    }
    /// <summary>
    /// Performs an operation on on all active entity/component pairs.
    /// </summary>
    /// <param name="operation"></param>
    public void ForEachComponent(Action<T> operation)
    {
        for (int i = 0; i < _activeCount; i++)
        {
            operation(Values[i]);
        }
    }

    /// <inheritdoc />
    public void Delete(EntityId id)
    {
        if (GetIdx(id, out var idx))
        {
            var swapIdx = _activeCount - 1;
            var tmpEntity = Entities[swapIdx];
            var tmpComponent = Values[swapIdx];

            Entities[swapIdx] = Entities[idx];
            Values[swapIdx] = Values[idx];

            Entities[idx] = tmpEntity;
            Values[idx] = tmpComponent;

            _activeCount -= 1;
        }
    }

    /// <summary>
    /// Returns whether the given entity has a component.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Has(EntityId id)
    {
        return GetIdx(id, out var _);
    }

    /// <summary>
    /// Fetches the id of the given entity.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool GetIdx(EntityId id, out int idx)
    {
        idx = -1;

        for (int i = 0; i < _activeCount; i++)
        {
            if (Entities[i] == id)
            {
                idx = i;
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public void Reset()
    {
        _activeCount = 0;
    }
}