using System;
using UnityEngine;
using Zenject;

public class ScenePresenter : MonoBehaviour {
    [SerializeField] private CannonActor _cannon;
    [SerializeField] private JetSpawner _jetSpawner;
    [SerializeField] private BombSpawner _bombSpawner;
    
    private PlayerModel _model;
    private GameLevel _currentScene;

    [Inject]
    private void Construct(PlayerModel model) {
        _model = model;
    }

    public void Create(LevelTemplate level, float cooldown, Action complete) {
        var template = level;

        if (template != null) {
            var scene = template.Create(transform);

            _currentScene = scene;
            _jetSpawner.Init(OnBombDropped);
            _cannon.Init(cooldown);
            _bombSpawner.AddListener(OnPlayerHit);
            _bombSpawner.OnIntercept(OnPlayerInterceptBomb);
            _jetSpawner.OnChangeState(_currentScene.Represent);
            Delay.Execute(2f, () => _cannon.gameObject.SetActive(true));
            
            complete?.Invoke();
            
            return;
        }

        throw new NullReferenceException();
    }
    
    private void OnPlayerInterceptBomb() {
        _model.SessionScore.Value += 5;
    }

    private void OnPlayerHit() {
        _model.Lives.Value--;
    }

    private void OnBombDropped(Vector3 atPosition, int toDirection, float speed) {
        _bombSpawner.Spawn(atPosition, toDirection, speed);
    }

    public void Dispose() {
        _cannon.gameObject.SetActive(false);
        _currentScene.Dispose();
        _jetSpawner.Dispose();
        _bombSpawner.Dispose();
        
        Destroy(_currentScene.gameObject);
        
        _currentScene = null;
    }
    
    private void OnEnable() {
        _model.AimPoint.Changed += OnAimPointChanged;
        _model.ButtonState.Changed += OnButtonStateChanged;
    }
    
    private void OnButtonStateChanged(PlayerButtonState state) {
        if (_cannon.CanShot() && state == PlayerButtonState.Release) {
            var origin = _cannon.Origin.position;
            var direction = _cannon.transform.up;
            _cannon.Release();
            _bombSpawner.Shoot(origin, direction);
        }
        
        if (state == PlayerButtonState.Hold) {
            _cannon.Hold();
        }
    }

    private void OnAimPointChanged(Vector3 point) {
        _cannon.RotateTowards(point);    
    }

    private void OnDisable() {
        _model.ButtonState.Changed -= OnButtonStateChanged;
        _model.AimPoint.Changed -= OnAimPointChanged;
    }

}
