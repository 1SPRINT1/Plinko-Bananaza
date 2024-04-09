using System;

public abstract class LayoutCommand<T> : ILayoutCommand<T>
{
    private readonly Action _onExecute;
    private T _state;

    public LayoutCommand(T state, Action onExecute)
    {
        _state = state;
        _onExecute = onExecute;
    }

    public T State
    {
        get => _state;
        set
        {
            if (!value.Equals(_state))
            {
                _state = value;
                _changed?.Invoke(_state);
            }
        }
    }

    public event Action<T> Changed
    {
        add
        {
            _changed += value;
            value?.Invoke(_state);
        }
        remove
        {
            _changed -= value;
            value?.Invoke(_state);
        }
    }

    public void Execute()
    {
        _onExecute.Invoke();
    }

    private event Action<T> _changed;

    
}

public class BindingDispose : IDisposable
{
    private readonly Action _onDispose;

    public BindingDispose(Action onDispose)
    {
        _onDispose = onDispose;
    }

    public void Dispose()
    {
        _onDispose?.Invoke();
    }
}

public class ButtonCommand : LayoutCommand<bool>, IButtonCommand
{
    private ButtonCommand(bool value, Action onExecute) : base(value, onExecute)
    {
    }

    public static IButtonCommand Create(Action onComplete)
    {
        return new ButtonCommand(true, onComplete);
    }

    public static IButtonCommand Create(bool state, Action onComplete)
    {
        return new ButtonCommand(state, onComplete);
    }

    public static IDisposable CreateBinding(Action<IButtonCommand> target, bool state, Action onComplete = null)
    {
        ButtonCommand command = new ButtonCommand(state, onComplete);

        target?.Invoke(command);

        return new BindingDispose(() => { target?.Invoke(null); });
    }
}

public class LabelCommand : LayoutCommand<string>, ILabelCommand
{
    private LabelCommand(string state, Action onExecute) : base(state, onExecute)
    {
    }

    public static ILabelCommand Create(Action onComplete = null)
    {
        return new LabelCommand("", onComplete);
    }

    public static ILabelCommand Create(string state, Action onComplete = null)
    {
        return new LabelCommand(state, onComplete);
    }

    public static IDisposable CreateBinding(ObservableInt model, Action<ILabelCommand> target, string state,
        Func<int, string> onChange = null, Action onComplete = null)
    {
        onChange ??= v => v.ToString();

        LabelCommand command = new LabelCommand(state, onComplete);
        model.Changed += OnModelFieldChanged;

        target?.Invoke(command);

        return new BindingDispose(() =>
        {
            model.Changed -= OnModelFieldChanged;
            target?.Invoke(null);
        });

        void OnModelFieldChanged(int value)
        {
            command.State = onChange.Invoke(value);
        }
    }

    public static IDisposable CreateBinding(ObservableUInt model, Action<ILabelCommand> target, string state,
        Func<uint, string> onChange = null, Action onComplete = null)
    {
        onChange ??= v => v.ToString();

        LabelCommand command = new LabelCommand(state, onComplete);
        model.Changed += OnModelFieldChanged;

        target?.Invoke(command);

        return new BindingDispose(() =>
        {
            model.Changed -= OnModelFieldChanged;
            target?.Invoke(null);
        });

        void OnModelFieldChanged(uint value)
        {
            command.State = onChange.Invoke(value);
        }
    }

    public static IDisposable CreateBinding(ObservableString model, Action<ILabelCommand> target, string state,
        Func<string, string> onChange = null, Action onComplete = null)
    {
        onChange ??= v => v.ToString();

        LabelCommand command = new LabelCommand(state, onComplete);
        model.Changed += OnModelFieldChanged;

        target?.Invoke(command);

        return new BindingDispose(() =>
        {
            model.Changed -= OnModelFieldChanged;
            target?.Invoke(null);
        });

        void OnModelFieldChanged(string value)
        {
            command.State = onChange.Invoke(value);
        }
    }
}

public class GroupBoxCommand : ValueCommand<byte>, IGroupBoxCommand
{
    public GroupBoxCommand(byte state, Action<byte> onExecute) : base(state, onExecute)
    {
    }

    public static IGroupBoxCommand Create(Action<byte> onComplete = null)
    {
        return new GroupBoxCommand(0, onComplete);
    }

    public static IGroupBoxCommand Create(byte state, Action<byte> onComplete = null)
    {
        return new GroupBoxCommand(state, onComplete);
    }
}