using System;

public interface IValueCommand<T>
{
    T Value { get; set; }

    event Action<T> Changed;
    void Execute(T value);
}