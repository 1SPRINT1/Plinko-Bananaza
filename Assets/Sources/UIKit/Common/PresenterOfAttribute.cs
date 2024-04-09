using System;

[AttributeUsage(AttributeTargets.Class)]
public class PresenterOfAttribute : Attribute
{
    public PresenterOfAttribute(params Type[] types)
    {
        Types = types;
    }

    public Type[] Types { get; }
}