[PresenterOf(typeof(TutorialMenuState))]
public class TutorialMenuPresenter : IPresenter {
    private readonly GameController _controller;
    private readonly TutorialMenuScreen _screen;
    private readonly IButtonCommand _continueCommand;

    public TutorialMenuPresenter(IAppStates states, GameController controller, TutorialMenuScreen screen) {
        _controller = controller;
        _screen = screen;

        _continueCommand = ButtonCommand.Create(states.ChangeState<PlayMenuState>);
    }
    
    public void Enter() {
        _controller.PauseGame(true);
        
        _screen.OnComplete(_continueCommand.Execute);
    }

    public void Exit() {
        _controller.PauseGame(false);
    }

}
