using System;

public class ScreenState<TScreen> where TScreen: MenuScreen {

    private readonly IPresenter[] _presenters;
    private readonly TScreen _view;

    protected ObservableBool IsActive { get; } = new(false);
    
    public ScreenState(IPresenter[] presenters, TScreen view) {
        _presenters = presenters;
        _view = view;
    }
    
    public void Enter(Action complete) {
        _presenters.Each(p => p.Enter());
        _view.Show(complete);
    }

    public void Sleep(bool state, Action complete) {
        IsActive.Value = state;
        complete?.Invoke();
    }

    public void Exit(Action complete) {
        _presenters.Each(p => p.Exit());
        _view.Hide(complete);
    }

}
