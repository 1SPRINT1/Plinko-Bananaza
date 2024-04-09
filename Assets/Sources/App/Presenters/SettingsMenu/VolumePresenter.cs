[PresenterOf(typeof(SettingsMenuState))]
public class VolumePresenter : IPresenter {

    private readonly PlayerModel _model;
    private readonly VolumePresenterView _view;
    
    private readonly IButtonCommand _returnBackCommand;

    public VolumePresenter(PlayerModel model, VolumePresenterView view) {
        _model = model;
        _view = view;
    }
    
    public void Enter() {
        _view.AddListener(_model.Volume.Value, (volume) => {
            _model.Volume.Value = volume;
        });
    }

    public void Exit() {}

}
