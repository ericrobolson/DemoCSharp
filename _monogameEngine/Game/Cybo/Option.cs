using System;

namespace Cybo;

/// <summary>
/// A basic Option type.
/// Preferable to using nullable fields.
/// </summary>
public struct Option<T>
{
    private bool _hasValue;
    private T? _value;

    /// <summary>
    /// Returns a 'None' value of an Option.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Option<T> None { get => new Option<T>(); }

    /// <summary>
    /// Returns a 'Some' value fo an Option.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Option<T> Some(T obj)
    {
        return new Option<T>(obj);
    }

    public Option(T value)
    {
        _hasValue = true;
        _value = value;
    }

    public Option()
    {
        _hasValue = false;
        _value = default(T);
    }

    /// <summary>
    /// Performs a match on the Option.
    /// </summary>
    /// <param name="some"></param>
    /// <param name="none"></param>
    public void Match(Action<T> some, Action none)
    {
        if (_hasValue && _value != null)
        {
            some(_value);
        }
        else
        {
            none();
        }
    }

    public void IsSome(Action<T> some)
    {
        Match(some, () => { });
    }
}