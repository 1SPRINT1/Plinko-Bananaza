using UnityEngine;

[PresenterOf(typeof(CompleteMenuState))]
public class CompleteMenuPresenter : IPresenter {

    private readonly PlayerModel _model;
    private readonly GameController _controller;
    private readonly CompleteMenuScreen _screen;
    
    private readonly IButtonCommand _retryCommand;
    private readonly IButtonCommand _exitToMenuCommand;
    private readonly IButtonCommand _continueCommand;

    public CompleteMenuPresenter(IAppStates states, PlayerModel model, GameController controller, CompleteMenuScreen screen) {
        _model = model;
        _controller = controller;
        _screen = screen;

        _retryCommand = ButtonCommand.Create(() => {
            screen.Hide(() => {
                controller.StartGame(model.Template, states.ChangeState<PlayMenuState>);    
            });
        });
        
        _exitToMenuCommand = ButtonCommand.Create(states.ChangeState<MainMenuState>);
        
        _continueCommand = ButtonCommand.Create(() => {
            screen.Hide(() => {
                model.CurrentLevel.Value = Mathf.Clamp(model.CurrentLevel.Value + 1, 0, 10);
                controller.StartGame(model.Template, states.ChangeState<PlayMenuState>);    
            });
        });
    }
    
    public void Enter() {
        _controller.EndGame();
        
        _screen.OnButtonClick<RetryGameButton>(_retryCommand);
        // TODO: Changed by game complete state
        

        var isWin = _model.GetLevelState();
        var before = _model.Score.Value;
        
        if(isWin) _model.ApplySession();
        
        _screen.PostScreen(isWin, before, _model.Score.Value);

        var nextCommand = isWin ? _continueCommand : _exitToMenuCommand;
        
        _screen.OnButtonClick<ContinueButton>(nextCommand);
    }

    public void Exit() {
        
    }

}
