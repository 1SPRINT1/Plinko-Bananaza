[PresenterOf(typeof(PlayMenuState), typeof(CompleteMenuState))]
public class LivesPresenter : IPresenter {
    private readonly PlayerModel _model;
    private readonly LivesPresenterView[] _view;

    public LivesPresenter(PlayerModel model, LivesPresenterView[] view) {
        _model = model;
        _view = view;
    }
    
    public void Enter() {
        _model.Lives.Changed += OnLivesAmountChanged;
    }
    private void OnLivesAmountChanged(int amount) {
        _view.Each(v => v.UpdateValue(amount));
    }

    public void Exit() {
        _model.Lives.Changed -= OnLivesAmountChanged;
    }

}
