[PresenterOf(typeof(PlayMenuState), typeof(CompleteMenuState))]
public class SessionScorePresenter : IPresenter {
    private readonly PlayerModel _model;
    private readonly SessionScorePresenterView[] _view;

    public SessionScorePresenter(PlayerModel model, SessionScorePresenterView[] view) {
        _model = model;
        _view = view;
    }
    
    public void Enter() {
        _model.SessionScore.Changed += OnScoreAmountChanged;
    }
    private void OnScoreAmountChanged(int amount) {
        _view.Each(v => v.UpdateValue(amount));
    }

    public void Exit() {
        _model.SessionScore.Changed -= OnScoreAmountChanged;
    }

}
