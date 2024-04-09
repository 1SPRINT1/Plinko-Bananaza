[PresenterOf(typeof(SettingsMenuState))]
public class SettingsMenuPresenter : IPresenter {

    private readonly SettingsMenuScreen _screen;
    
    private readonly IButtonCommand _returnBackCommand;

    public SettingsMenuPresenter(IAppStates states, SettingsMenuScreen screen) {
        _screen = screen;

        _returnBackCommand = ButtonCommand.Create(states.ReturnBack);
    }
    
    public void Enter() {
        _screen.OnButtonClick<ReturnBackButton>(_returnBackCommand);
    }

    public void Exit() {}

}