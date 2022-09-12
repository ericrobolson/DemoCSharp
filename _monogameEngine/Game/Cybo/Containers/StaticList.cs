using System.Collections.Generic;

namespace Cybo.Containers;

/// <summary>
/// A simple static list that allocates a collection with the given capacity.
/// </summary>
/// <typeparam name="T"></typeparam>
public class StaticList<T> where T : new()
{
    private int _activeItems;
    private List<T> _items;
    private readonly int _capacity;


    public StaticList(int capacity)
    {

        if (capacity < 0)
        {
            throw new System.Exception($"A {typeof(StaticList<T>).Name} requires a positive capacity.");
        }

        _capacity = capacity;
        _items = new List<T>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            _items.Add(new T());
        }
    }

    public int Count { get => _items.Count; }

    public T this[int index] { get => _items[index]; set => _items[index] = value; }


    /// <summary>
    /// Returns the given range of items.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public List<T> GetRange(int index, int count)
    {
        return _items.GetRange(index, count);
    }

}