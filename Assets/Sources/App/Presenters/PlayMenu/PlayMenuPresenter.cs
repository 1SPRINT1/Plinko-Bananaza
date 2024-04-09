using UnityEngine;

[PresenterOf(typeof(PlayMenuState))]
public class PlayMenuPresenter : IPresenter {

    private readonly PlayerModel _model;
    private readonly PlayMenuScreen _screen;
    
    private readonly IButtonCommand _pauseGameCommand;
    private readonly IButtonCommand _completeGameCommand;
    private LevelTemplate _template;
    private readonly IButtonCommand _tutorialCommand;
    private bool _tutorialInProgress;
    
    public PlayMenuPresenter(IAppStates states, PlayerModel model, PlayMenuScreen screen) {
        _model = model;
        _screen = screen;
        
        _pauseGameCommand = ButtonCommand.Create(states.ChangeState<PauseMenuState>);
        _completeGameCommand = ButtonCommand.Create(states.ChangeState<CompleteMenuState>);

        _tutorialCommand = ButtonCommand.Create(states.ChangeState<TutorialMenuState>);
    }
    
    public void Enter() {
        
        if (_model.CurrentLevel.Value == 0 && !_model.TutorialComplete) {
            _model.TutorialComplete = true;
            Delay.Execute(.25f, _tutorialCommand.Execute);
            return;
        }
        
        MusicPlayer.Play();
        
        _template = _model.Template;
        
        _screen.OnButtonClick<PauseGameButton>(_pauseGameCommand);
        _screen.SubscribeAim((v) => {
            var position = Vector3.right * v.x;
            _model.AimPoint.Value = position;
        });
        
        _screen.SubscribeToAction(action => {
            _model.ButtonState.Value = action;
        });

        _model.SessionScore.Changed += OnSessionScoreChanged;
        _model.Lives.Changed += OnPlayerLivesChanged;
    }
    
    private void OnPlayerLivesChanged(int lives) {
        if (lives <= 0) {
            Delay.Execute(.25f, _completeGameCommand.Execute);
        }
    }

    private void OnSessionScoreChanged(int score) {
        if (score >= _template.TargetScore) {
            Delay.Execute(.25f, _completeGameCommand.Execute);
        }
    }

    public void Exit() {
        MusicPlayer.FadeOut();
        
        _model.Lives.Changed -= OnPlayerLivesChanged;
        _model.SessionScore.Changed -= OnSessionScoreChanged;
        _screen.UnSubscribeAll();
    }

}