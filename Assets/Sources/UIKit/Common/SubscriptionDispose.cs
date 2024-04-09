using System;

public class SubscriptionDispose : IDisposable {
    private readonly Action _onDispose;

    public SubscriptionDispose(Action onDispose) => _onDispose = onDispose;

    public void Dispose() => _onDispose?.Invoke();

}
