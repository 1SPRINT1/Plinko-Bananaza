[PresenterOf(typeof(ShopMenuState))]
public class ShopMenuPresenter : IPresenter {

    private readonly PlayerModel _model;
    private readonly ShopMenuScreen _screen;
    
    private readonly IButtonCommand _returnBackCommand;
    private readonly IButtonCommand _upgradeCommand;

    public ShopMenuPresenter(IAppStates states, PlayerModel model, ShopMenuScreen screen) {
        _model = model;
        _screen = screen;

        _returnBackCommand = ButtonCommand.Create(states.ReturnBack);
        _upgradeCommand = ButtonCommand.Create(OnUpgradeCooldown);
    }
    
    private void OnUpgradeCooldown() {
        _upgradeCommand.State = _model.UpgradeCooldown();
    }

    public void Enter() {
        _screen.OnButtonClick<ReturnBackButton>(_returnBackCommand);
        _screen.OnButtonClick<UpgradeCooldownButton>(_upgradeCommand);

        _model.Score.Changed += OnPlayerScoreChanged;
    }
    
    private void OnPlayerScoreChanged(int score) {
        _upgradeCommand.State = _model.CooldownUpgradeIsPossible;
    }

    public void Exit() {
        _model.Score.Changed -= OnPlayerScoreChanged;
    }

}