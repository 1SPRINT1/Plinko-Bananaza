using System;

public class ValueCommand<T> : IValueCommand<T>
{
    private readonly Action<T> _onExecute;
    private T _value;

    public ValueCommand(T value, Action<T> onExecute)
    {
        _value = value;
        _onExecute = onExecute;
    }

    public T Value
    {
        get => _value;
        set
        {
            if (!value.Equals(_value))
            {
                _value = value;
                _changed?.Invoke(_value);
            }
        }
    }

    public event Action<T> Changed
    {
        add
        {
            _changed += value;
            value?.Invoke(_value);
        }
        remove
        {
            _changed -= value;
            value?.Invoke(_value);
        }
    }

    public void Execute(T value)
    {
        _onExecute.Invoke(value);
    }

    private event Action<T> _changed;
}