using System;
using UnityEngine;

public class PlayMenuScreen : MenuScreen {
    [SerializeField] private AimHandler _aim;

    public void SubscribeAim(Action<Vector3> onAimChanged) => _aim.Subscribe(onAimChanged);
    public void UnSubscribeAll() {
        _aim.Subscribe(null);
        _aim.SubscribeAction(null);
    }
    public void SubscribeToAction(Action<PlayerButtonState> onState) => _aim.SubscribeAction(onState);
}