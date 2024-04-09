using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Jet : MonoBehaviour {
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _maxDropDistance = .5f;
    [SerializeField] private float _minDropDistance = .25f;

    [Space]
    [SerializeField] private AudioSource _source;
    
    private Action _complete;
    
    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private Action<Vector3, int, float> _onDrop;
    private float _totalDistance;
    private bool _drop;
    private int _direction;

    public void AddListener(Action<Vector3, int, float> onDrop) {
        _onDrop = onDrop;
    }
    
    public void FlyOver(Vector3 bounds, Vector3 line, Action complete) {
        _complete = complete;
        
        var offset = bounds.x * .75f;
        
        _direction = Random.Range(0, 2) == 1? 1: -1;
        
        _drop = false;
        _startPoint = new Vector3(offset * _direction, line.y);
        _endPoint = new Vector3(offset * -_direction, line.y);
        _totalDistance = Vector3.Distance(_startPoint, _endPoint);
        
        transform.position = _startPoint;
        transform.right = (_startPoint - _endPoint).normalized;
        
        _source.Play();
    }

    private void FixedUpdate() {
        if (_complete == null) return;

        transform.position = Vector3.MoveTowards(transform.position, _endPoint, Time.fixedDeltaTime * _movementSpeed);

        var distance = Vector3.Distance(_endPoint, transform.position);

        if (_drop == false) {
            var path = 1- distance / _totalDistance;

            if (path > _maxDropDistance || (path > _minDropDistance && Random.Range(0, 5) == 4)) {
                Drop();
            }
        }
        
        if (distance < .01f) {
            _complete();
            _complete = null;
            _source.Stop();
        }

        void Drop() {
            _onDrop?.Invoke(transform.position, _direction, _movementSpeed);
            _drop = true;
        }
    }
}
