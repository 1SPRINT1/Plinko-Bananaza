using System;

public interface ILayoutCommand<T>
{
    T State { get; set; }

    event Action<T> Changed;

    void Execute();
}