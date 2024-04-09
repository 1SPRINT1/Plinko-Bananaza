using System;
using UnityEngine;

public class AimHandler : MonoBehaviour {
    
    private Plane _plane;
    private Action<Vector3> _onPosition;
    private Action<PlayerButtonState> _onState;

    private PlayerButtonState _current;
    
    private void Awake() => _plane = new Plane(Vector3.back, Vector3.zero);

    public void Subscribe(Action<Vector3> onPosition) => _onPosition = onPosition;

    private void Update() {

        var state = Input.GetMouseButtonDown(0) ? PlayerButtonState.Hold :
            Input.GetMouseButtonUp(0) ? PlayerButtonState.Release : PlayerButtonState.None;

        if (state != _current) {
            _current = state;
            _onState?.Invoke(_current);
        }
        
        if (Input.GetMouseButton(0)) {
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            _plane.Raycast(ray, out var distance);
            
            _onPosition?.Invoke(ray.GetPoint(distance));
        }
    }

    public void SubscribeAction(Action<PlayerButtonState> onState) => _onState = onState;
}
