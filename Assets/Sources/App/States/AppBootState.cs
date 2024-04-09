using System;

public class AppBootState : IBootState {

    private readonly AppBootScreen _screen;
    private readonly IButtonCommand _continueLoadingCommand;

    public AppBootState(IAppStates states, AppBootScreen screen) {
        _screen = screen;

        _continueLoadingCommand = ButtonCommand.Create(states.ChangeState<MainMenuState>);
    }
    
    public void Enter(Action complete) {
        _screen.Show(complete);

        Delay.Execute(1f, _continueLoadingCommand.Execute);
    }

    public void Sleep(bool state, Action complete) => complete?.Invoke();

    public void Exit(Action complete) => _screen.Hide(complete);

}