[PresenterOf(typeof(PauseMenuState))]
public class PauseMenuPresenter : IPresenter {

    private readonly GameController _controller;
    private readonly PauseMenuScreen _screen;
    
    private readonly IButtonCommand _returnBackCommand;
    private readonly IButtonCommand _cancelGameCommand;
    private readonly IButtonCommand _showSettingsCommand;

    public PauseMenuPresenter(IAppStates states, GameController controller, PauseMenuScreen screen) {
        _controller = controller;
        _screen = screen;

        _returnBackCommand = ButtonCommand.Create(() => {
            _controller.PauseGame(false);
            states.ChangeState<PlayMenuState>();
        });
        _showSettingsCommand = ButtonCommand.Create(states.ChangeState<SettingsMenuState>);
        _cancelGameCommand = ButtonCommand.Create(states.ChangeState<CompleteMenuState>);
    }
    
    public void Enter() {
        _controller.PauseGame(true);
        
        _screen.OnButtonClick<ReturnBackButton>(_returnBackCommand);
        _screen.OnButtonClick<CancelGameButton>(_cancelGameCommand);
        _screen.OnButtonClick<ShowSettingsButton>(_showSettingsCommand);
    }

    public void Exit() {}

}