[PresenterOf(typeof(ShopMenuState))]
public class ShopPresenter : IPresenter {
    private readonly PlayerModel _model;
    private readonly ShopPresenterView _view;

    public ShopPresenter(PlayerModel model, ShopPresenterView view) {
        _model = model;
        _view = view;
    }
    
    public void Enter() {
        _model.CooldownLevel.Changed += OnCooldownValueChanged;
    }
    private void OnCooldownValueChanged(int amount) {
        _view.SetProgress((amount + 1) / 5f);
    }

    public void Exit() {
        _model.CooldownLevel.Changed -= OnCooldownValueChanged;
    }

}