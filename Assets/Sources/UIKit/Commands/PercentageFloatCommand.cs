using System;

public class PercentageFloatCommand : LayoutCommand<float>, IFloatCommand
{
    private PercentageFloatCommand(int value, int maxValue, Action onExecute) : base(value, onExecute)
    {
    }

    public static IFloatCommand Create(int value, int maxValue, Action onExecute)
    {
        return new PercentageFloatCommand(value, maxValue, onExecute);
    }

    public static IFloatCommand Create(int maxValue, Action onExecute)
    {
        return new PercentageFloatCommand(0, maxValue, onExecute);
    }
    
    public static IDisposable CreateBinding(ObservableInt model, Action<IFloatCommand> target, int value, int maxValue, Action onComplete = null) 
    {
        PercentageFloatCommand command = new PercentageFloatCommand(value, maxValue, onComplete);
        model.Changed += OnModelFieldChanged;
        
        target?.Invoke(command);
        
        return new BindingDispose(() =>
        {
            model.Changed -= OnModelFieldChanged;
            target?.Invoke(null);
        });
        
        void OnModelFieldChanged(int value)
        {
            command.State = (float)value/maxValue;
        }
    }
}