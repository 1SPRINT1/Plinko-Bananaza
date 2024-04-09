using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class JetSpawner : MonoBehaviour {
    [SerializeField] private Jet[] _jets;
    [SerializeField] private int _min = 3;
    [SerializeField] private int _max = 5;
    
    private Vector3[] _lines;
    
    private float _nextSpawn = 0f;
    private Vector2 _bounds;
    private Jet _jet;
    private bool _running;
    private Action _onChangeState;

    private void Awake() {
        _bounds = Camera.main.GetCameraProjection();
        var height = (_bounds.y - 4) * .5f;

        _lines = new[] {
            Vector3.up * height,
            Vector3.up * (height - .5f)
        };
    }

    public void Init(Action<Vector3, int, float> onDrop) {
        _jets.Each(j => j.AddListener(onDrop));

        Delay.Execute(1f, () => {
            _running = true;
            SetNextTime();
        });
    }

    private void SetNextTime() {
        if (!_running) return;
        _nextSpawn = Time.time + Random.Range(_min, _max);
    }

    private void Update() {
        if (_running && _jet == null && _nextSpawn < Time.time) {
            _jet = _jets.Shuffle(5).FirstOrDefault();
            var line = _lines.Shuffle(3).FirstOrDefault();

            _jet.FlyOver(_bounds, line, OnFlyComplete);
        }
    }
    
    private void OnFlyComplete() {
        _onChangeState?.Invoke();
        
        SetNextTime();
        _jet = null;
    }

    public void Dispose() {
        _running = false;
        _jets.Each(j => j.AddListener(null));
    }
    
    public void OnChangeState(Action onChangeState) {
        _onChangeState = onChangeState;
    }
}
