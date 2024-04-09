using System;
using UnityEngine;
using Zenject;

public class AppEntry : MonoBehaviour, IInitializable, IAppStates {
    [SerializeField] private bool _usePausedState;
    
    private AppClient _client;
    private Action _focusChanged;
    private PlayerModel _model;

    [Inject]
    private void Construct(AppClient client, PlayerModel model) {
        _model = model;
        _client = client;

        _client.StateChanged += OnStateChanged;
    }
    
    private void OnStateChanged(IScreenState state) {
        _model.Save();
    }

    public void Initialize() {
        _client.Start();        
    }

    private void OnApplicationFocus(bool hasFocus) {
        if(hasFocus)
            _focusChanged?.Invoke();

        if (_usePausedState && !hasFocus && _client.Is<PlayMenuState>()) {
            ChangeState<PauseMenuState>();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            _model.Score.Value += 50;
        }
    }

    private void OnApplicationQuit() {
        _client.StateChanged -= OnStateChanged;
        _client.Stop();
    }

    public void ChangeState<TState>() where TState : class, IScreenState => _client.Change<TState>();
    public void ShowPopup<TState>() where TState : class, IScreenState => _client.Overlap<TState>();

    public void ReturnBack() => _client.ReturnBack();

}
