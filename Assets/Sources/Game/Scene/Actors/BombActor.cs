using System;
using System.Collections.Generic;
using UnityEngine;

public class BombActor : MonoBehaviour {
    [SerializeField] private Rigidbody2D _rb;
    
    private readonly HashSet<StickActor> _sticks = new();
    private Camera _camera;
    
    private Action<BombActor, bool> _onDestroy;

    private void OnValidate() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Awake() {
        _camera = Camera.main;
    }
    
    public void Force(Vector3 direction) {
        _rb.AddForce(direction * 10, ForceMode2D.Impulse);
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.TryGetComponent<StickActor>(out var stick) && _sticks.Add(stick)) {
            stick.Apply();
        }

        if (this is PlayerBomb && other.collider.TryGetComponent<JetBomb>(out _)) {
            Effects.Execute<SelfDestroyEffect>(transform.position);
            _onDestroy?.Invoke(this, true);
        }
        
        if (this is JetBomb && other.collider.TryGetComponent<PlayerBomb>(out _) ) {
            _onDestroy?.Invoke(this, false);
        }
    }

    public void AddListener(Action<BombActor, bool> onDestroy) => _onDestroy = onDestroy;

    private void Update() {
        var height = transform.position.y;
        var bounds = _camera.GetCameraProjection();

        if (this is JetBomb && height <= bounds.y * -.45f && transform.position.x < bounds.x * .5f && transform.position.x > bounds.x * -.5f) {
            
            Effects.Execute<PlayerHitEffect>(transform.position);
            
            _onDestroy?.Invoke(this, true);
            return;
        }
        
        if (transform.position.y < -_camera.orthographicSize) {
            
            _onDestroy?.Invoke(this, false);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.collider.TryGetComponent<StickActor>(out var stick) && _sticks.Contains(stick)) {
            _sticks.Remove(stick);
        }
    }

    
}