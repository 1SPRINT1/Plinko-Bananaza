using System;
using System.Collections.Generic;
using System.Linq;

public interface IAppStates
{
    void ChangeState<TState>() where TState : class, IScreenState;
    void ShowPopup<TState>() where TState : class, IScreenState;
    void ReturnBack();
}

public interface IBootState : IScreenState {}

public interface IScreenState
{
    void Enter(Action complete);
    void Sleep(bool state, Action complete);
    void Exit(Action complete);
}

public abstract class ScreenStates<T> where T : class, IScreenState
{
    private readonly HashSet<T> _states = new();
    private readonly Stack<T> _history = new();
    
    private T _previous;
    private T _state;
    private T _activeState;
    public bool Is<T>() => _state is T;
    public event Action<T> StateChanged;

    protected void AddToStates(T state)
    {
        _states.Add(state);
    }

    // TODO: can be replaced with commands history
    public void ReturnBack()
    {
        if(_history.Count > 0)
            ChangeState(_history.Pop());
    }

    public void Change<TState>() where TState : class, IScreenState
    {
        T state = _states.OfType<TState>().FirstOrDefault() as T;

        if(_state != null) _history.Push(_state);
        
        ChangeState(state);
    }

    public void Overlap<TState>() where TState : class, IScreenState
    {
        T state = _states.OfType<TState>().FirstOrDefault() as T;

        OverlapState(state);
    }
    
    public void Start()
    {
        IBootState state = _states.OfType<IBootState>().FirstOrDefault();

        if (state is T bootState)
            ChangeState(bootState);

        OnStart();
    }

    protected virtual void OnStart() {}

    public void Stop()
    {
        ChangeState(null);
        OnEnd();
    }

    protected virtual void OnEnd() {}

    private void OverlapState(T state) {
        if (_activeState == null) {
            _previous = _state;
            
            _previous.SafeSuspend(() => {
                state.SafeEnter(() => {
                    _activeState = state;
                });
            });

            return;
        }
        
        _activeState.SafeExit(() => {
            _previous.SafeResume(() => {
                _activeState = null;
                OverlapState(state);
            });
        });
    }
    
    private void ChangeState(T state)
    {
        if (_activeState != null) {
            _activeState.SafeExit(() => {
                _previous.SafeResume(() => {
                    _activeState = null;
                    ChangeState(state);
                });
            });
            return;
        }
        
        if (state != _state)
        {
            _previous = _state;

            _state.SafeExit(() =>
            {
                _state = state;
                _state.SafeEnter(() => StateChanged?.Invoke(_state));
            });
        }
    }
}