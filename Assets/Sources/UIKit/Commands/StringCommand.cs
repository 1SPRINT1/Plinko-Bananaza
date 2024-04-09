using System;

public class StringCommand : LayoutCommand<string>, IStringCommand
{
    private StringCommand(string value, Action onExecute) : base(value, onExecute)
    {
    }

    public static IStringCommand Create(string state, Action onExecute)
    {
        return new StringCommand(state, onExecute);
    }

    public static IStringCommand Create(Action onExecute)
    {
        return new StringCommand("", onExecute);
    }
}