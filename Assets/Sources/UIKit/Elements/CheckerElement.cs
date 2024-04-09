using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class CheckerElement : MonoBehaviour, ILayoutElement
{
    [SerializeField] [HideInInspector] private Button _button;
    private bool _buttonState;

    private IButtonCommand _command;

    private void OnValidate()
    {
        _button = GetComponent<Button>();
        name = $"<{GetType().Name}>";
    }

    public void OnShowElement()
    {
    }

    public void OnHideElement()
    {
        if (_command != null)
            _command.Changed -= OnButtonStateChanged;

        if (_button != null && _command != null)
            _button.onClick.RemoveAllListeners();
    }

    public void OnClick(IButtonCommand command)
    {
        _command = command;

        if (_command != null)
            _command.Changed += OnButtonStateChanged;

        if (_button != null && _command != null)
            _button.onClick.AddListener(_command.Execute);
    }

    private void OnButtonStateChanged(bool state)
    {
        _buttonState = state;
        OnChangeButtonInteractivity(state);
    }

    protected virtual void OnChangeButtonInteractivity(bool state)
    {
    }
}