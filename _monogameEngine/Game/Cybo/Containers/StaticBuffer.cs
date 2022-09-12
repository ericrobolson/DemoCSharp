using System;
using System.Collections.Generic;

namespace Cybo.Containers;

/// <summary>
/// A simple static buffer that allocates a collection and allows inserting of new items.
/// </summary>
/// <typeparam name="T"></typeparam>
public class StaticBuffer<T> where T : new()
{
    private int _activeItems;
    private List<T> _items;
    private readonly int _capacity;


    public StaticBuffer(int capacity)
    {

        if (capacity < 0)
        {
            throw new System.Exception($"A {typeof(StaticBuffer<T>).Name} requires a positive capacity.");
        }

        _capacity = capacity;
        _items = new List<T>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            _items.Add(new T());
        }
    }

    /// <summary>
    /// Returns the items in the static buffer.
    /// </summary>
    /// <value></value>
    public int Count { get => _activeItems; }

    /// <summary>
    /// Returns the active items.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public List<T> Items { get => _items.GetRange(0, _activeItems); }

    /// <summary>
    /// Resets the buffer.
    /// </summary>
    public void Clear()
    {
        _activeItems = 0;
    }

    /// <summary>
    /// Attempts to add the given element to the list.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public bool Add(Func<T, T> action)
    {
        if (_activeItems < _capacity)
        {
            var idx = _activeItems;
            _items[idx] = action(_items[idx]);
            _activeItems += 1;
            return true;
        }

        return false;
    }

}