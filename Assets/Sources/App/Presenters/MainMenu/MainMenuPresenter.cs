[PresenterOf(typeof(MainMenuState))]
public class MainMenuPresenter : IPresenter {

    private readonly MainMenuScreen _screen;
    
    private readonly IButtonCommand _playGameCommand;
    private readonly IButtonCommand _showSettingsCommand;
    private readonly IButtonCommand _showShopCommand;

    public MainMenuPresenter(IAppStates states, PlayerModel model, GameController controller, MainMenuScreen screen) {
        _screen = screen;

        _playGameCommand = ButtonCommand.Create(() => {
            screen.Hide(() => {
                controller.StartGame(model.Template,states.ChangeState<PlayMenuState>);    
            });
        });
        _showSettingsCommand = ButtonCommand.Create(states.ChangeState<SettingsMenuState>);
        _showShopCommand = ButtonCommand.Create(states.ChangeState<ShopMenuState>);
    }
    
    public void Enter() {
        _screen.OnButtonClick<PlayGameButton>(_playGameCommand);
        _screen.OnButtonClick<ShowSettingsButton>(_showSettingsCommand);
        _screen.OnButtonClick<ShowShopButton>(_showShopCommand);
    }

    public void Exit() {}
}