using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderElement : MonoBehaviour, ILayoutElement
{
    [SerializeField] [HideInInspector] private Slider _slider;

    private IValueCommand<float> _command;

    private void OnValidate()
    {
        _slider = GetComponent<Slider>();
        name = $"<{GetType().Name}>";
    }

    public void OnShowElement()
    {
    }

    public void OnHideElement()
    {
        if (_command != null)
            _command.Changed -= OnButtonStateChanged;

        if (_slider != null && _command != null)
            _slider.onValueChanged.RemoveAllListeners();
    }

    public void OnChange(IValueCommand<float> command)
    {
        _command = command;

        if (_command != null)
            _command.Changed += OnButtonStateChanged;

        if (_slider != null && _command != null)
            _slider.onValueChanged.AddListener(_command.Execute);
    }

    private void OnButtonStateChanged(float value)
    {
        _slider.value = value;
        OnChangeButtonInteractivity(value);
        OnSliderValueChanged(value);
    }

    protected virtual void OnSliderValueChanged(float value) 
    {
        
    }

    protected virtual void OnChangeButtonInteractivity(float state)
    {
    }
}