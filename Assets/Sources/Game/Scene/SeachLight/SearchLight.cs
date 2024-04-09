using UnityEngine;

public class SearchLight : MonoBehaviour {
    private float _nextTime;
    private int _maxAngle;
    private int _speed = 180;

    private void SetNextLoop() {
        _nextTime = Time.time + Random.Range(10, 15);
    }
    private void Update() {

        if (_nextTime < Time.time) {
            SetNextLoop();
            _maxAngle = Random.Range(-45, 45);
            _speed = Random.Range(180, 360);
        }
        
        var sin = Mathf.Sin(Time.time);
        var rotation = Mathf.Lerp(-_maxAngle, _maxAngle, (sin + 1) / 2);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.back * rotation), Time.deltaTime * _speed);
    }
}
