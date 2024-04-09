using System;
using UnityEngine;
using UnityEngine.UI;

public class VolumePresenterView : PresenterView {
    [SerializeField] private Slider _slider;
    private Action<float> _onChange;

    protected override void OnPresenterShow() {
        _slider.onValueChanged.AddListener((value) => _onChange?.Invoke(value));
    }

    public void AddListener(float value, Action<float> onChange) {
        _onChange = onChange;
        _slider.value = value;
    }

    protected override void OnPresenterHide() {
        _slider.onValueChanged.RemoveAllListeners();
    }
}
