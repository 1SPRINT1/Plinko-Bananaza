using System;
using UnityEngine;

public class GameController {
    private readonly HudScreen _screen;
    private readonly PlayerModel _model;
    private readonly ScenePresenter _presenter;

    public GameController(HudScreen screen, PlayerModel model, ScenePresenter presenter) {
        _screen = screen;
        _model = model;
        _presenter = presenter;
    }

    public void StartGame(LevelTemplate template, Action onReady) {
        _screen.Show(() => {
            _model.ResetSession();
            var cooldown = 1 - (_model.CooldownLevel.Value / 5f);
            
            _screen.PresentLevel(template.LevelTitle, template.TargetScore, () => {
                _presenter.Create(template, cooldown, onReady);
            });
        });
    }

    public void PauseGame(bool state) {
        // set pause time scale, dotween option: <time-scale independent> must be set for ignore time scale param;
        Time.timeScale = state ? 0 : 1;
    }

    public void EndGame() {
        _presenter.Dispose();
        PauseGame(false);
        
        _screen.Hide(() => {
            Debug.Log("Game Ended");
            _screen.Dispose();
            
        });
    }
    
}
