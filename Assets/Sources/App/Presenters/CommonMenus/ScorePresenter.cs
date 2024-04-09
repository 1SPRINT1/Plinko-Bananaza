[PresenterOf(typeof(ShopMenuState), typeof(PlayMenuState), typeof(CompleteMenuState))]
public class ScorePresenter : IPresenter {
    private readonly PlayerModel _model;
    private readonly ScorePresenterView[] _view;

    public ScorePresenter(PlayerModel model, ScorePresenterView[] view) {
        _model = model;
        _view = view;
    }
    
    public void Enter() {
        _model.Score.Changed += OnScoreAmountChanged;
    }
    
    private void OnScoreAmountChanged(int amount) {
        _view.Each(v => v.UpdateValue(amount));
    }

    public void Exit() {
        _model.Score.Changed -= OnScoreAmountChanged;
    }

}