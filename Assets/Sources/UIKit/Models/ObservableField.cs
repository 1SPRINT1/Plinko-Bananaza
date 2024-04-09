using System;
using System.Collections;
using System.Collections.Generic;

public interface IChangeNotifiable<T> {

    void Change();

}

public class FieldChangeNotification<TSource, TRelative> : IDisposable {

    private readonly ObservableField<TSource> _source;
    private readonly TRelative _relative;
    private Action<TRelative, TSource> _onChange;

    public FieldChangeNotification(ObservableField<TSource> source, TRelative relative) {
        _source = source;
        _relative = relative;
    }

    public FieldChangeNotification<TSource, TRelative> Subscribe(Action<TRelative, TSource> onChange) {
        _onChange = onChange;
        _source.Changed += OnChanged;
        return this;
    }

    private void OnChanged(TSource value) {
        _onChange?.Invoke(_relative, value);
    }

    public void Dispose() {
        _source.Changed -= OnChanged;
        _onChange = null;
    }
}

public static class ObservableFieldsExtensions {

    public static IDisposable Subscribe<TSource, TRelative>(this ObservableField<TSource> source, TRelative relativeSource, Action<TRelative, TSource> onChange) {
        return new FieldChangeNotification<TSource, TRelative>(source, relativeSource)
            .Subscribe(onChange);
    } 

}

public interface IObservableField<T> {
    T Value { get; set; }
    event Action<T> Changed;
}
public abstract class ObservableField<T> : IObservableField<T> {

    private T _value;
    protected bool NotifyWhenSubscription = true;

    protected bool UpdateAlways;

    public ObservableField(T value = default)
    {
        Value = value;
    }

    public T Value
    {
        get => _value;
        set
        {
            _updated?.Invoke(value);
            if (!UpdateAlways && value != null && value.Equals(_value)) return;

            _value = value;
            _changed?.Invoke(_value);
            _fieldChanged?.Invoke(this, _value);
        }
    }

    private event Action<T> _changed;
    private event Action<T> _updated;
    private event Action<ObservableField<T>, T> _fieldChanged;

    public event Action<T> Changed
    {
        add
        {
            _changed += value;
            if (NotifyWhenSubscription)
                value?.Invoke(_value);
        }
        remove
        {
            _changed -= value;
        }
    }
    
    public event Action<T> Updated
    {
        add
        {
            _updated += value;
            if (NotifyWhenSubscription)
                value?.Invoke(_value);
        }
        remove
        {
            _updated -= value;
            if (NotifyWhenSubscription)
                value?.Invoke(_value);
        }
    }
    
    public event Action<ObservableField<T>, T> FieldChanged
    {
        add
        {
            _fieldChanged += value;
            if (NotifyWhenSubscription)
                value?.Invoke(this, _value);
        }
        remove
        {
            _fieldChanged -= value;
            if (NotifyWhenSubscription)
                value?.Invoke(this, _value);
        }
    }
}

public class ObservableInt : ObservableField<int>
{
    public ObservableInt(int value = 0, bool updateAlways = false,  bool notify = true) : base(value)
    {
        UpdateAlways = updateAlways;
        NotifyWhenSubscription = notify;
    }
}

public class ObservableBool : ObservableField<bool>
{
    public ObservableBool(bool value = true) : base(value)
    {
    }
}

public class ObservableString : ObservableField<string>
{
    public ObservableString(string value = "", bool updateAlways = false,  bool notify = true) : base(value)
    {
        UpdateAlways = updateAlways;
        NotifyWhenSubscription = notify;
    }
}

public class ObservableClass<TClass> : ObservableField<TClass> where TClass : class {
    public ObservableClass(TClass value = default, bool updateAlways = false, bool notify = true) :
        base(value)
    {
        NotifyWhenSubscription = notify;
        UpdateAlways = updateAlways;
    }
}

public class ObservableStruct<TStruct> : ObservableField<TStruct> where TStruct : struct {
    public ObservableStruct(TStruct value = default, bool updateAlways = false, bool notify = true) :
        base(value)
    {
        NotifyWhenSubscription = notify;
        UpdateAlways = updateAlways;
    }
}

public class ObservableFloat : ObservableField<float>
{
    public ObservableFloat(float value = 0) : base(value)
    {
    }
}

public class ObservableEnum<T> : ObservableField<T> where T : Enum
{
    public ObservableEnum(T defaultValue = default) : base(defaultValue)
    {
    }
}

public class ObservableRecord<T> : ObservableField<T>
{
}

public class ObservableByte : ObservableField<byte>
{
    public ObservableByte(byte value = 0, bool updateAlways = false, bool notify = true) : base(value)
    {
        NotifyWhenSubscription = notify;
        UpdateAlways = updateAlways;
    }
}

public class ObservableUShort : ObservableField<ushort>
{
    public ObservableUShort(ushort value = 0) : base(value)
    {
    }
}

public class ObservableUInt : ObservableField<uint>
{
    public ObservableUInt(uint value = 0) : base(value)
    {
    }
}

public abstract class ObservableCollection<T> : IEnumerable<T>
{
    protected readonly List<T> _collection = new();
    protected bool NotifyWhenSubscription = true;

    public IEnumerator<T> GetEnumerator()
    {
        return _collection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public T[] Value
    {
        get => _collection.ToArray();
        set
        {
            _collection.Clear();
            AddRange(value);
        }
    }

    private event Action<T[]> _changed;

    public event Action<T[]> Changed
    {
        add
        {
            _changed += value;
            if (NotifyWhenSubscription)
                value?.Invoke(_collection.ToArray());
        }
        remove
        {
            _changed -= value;
            if (NotifyWhenSubscription)
                value?.Invoke(_collection.ToArray());
        }
    }

    public virtual void Add(T element)
    {
        _collection.Add(element);
        _changed?.Invoke(_collection.ToArray());
    }
    
    
    public void AddRange(IEnumerable<T> elements)
    {
        _collection.AddRange(elements);
            
        _changed?.Invoke(_collection.ToArray());
    }

    public void Clear()
    {
        _collection.Clear();
        _changed?.Invoke(_collection.ToArray());
    }

    public void Remove(T element)
    {
        _collection.Remove(element);
        _changed?.Invoke(_collection.ToArray());
    }
}